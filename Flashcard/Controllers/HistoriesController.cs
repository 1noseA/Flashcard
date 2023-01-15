﻿using Microsoft.AspNetCore.Mvc;
using Flashcard.Data;
using System.Security.Claims;
using Flashcard.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.IO;
using ClosedXML.Excel;
using Flashcard.Models;
using DocumentFormat.OpenXml;
using System;
using System.Reflection;
using Microsoft.VisualBasic;
using System.Diagnostics;
using System.Linq;

namespace Flashcard.Controllers
{
    public class HistoriesController : Controller
    {
        private readonly FlashcardContext _context;

        private readonly Claim _claim;

        private readonly IXLWorkbook _xlWorkbook;

        HistoriesViewModel viewModel = new HistoriesViewModel();

        public HistoriesController(FlashcardContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _claim = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        }

        // 初期処理
        // GET: Histories
        public async Task<IActionResult> Index()
        {
            // ログインしていなければログイン画面へ
            if (_claim == null)
            {
                return RedirectToAction("Index", "Account");
            }

            // 履歴を取得する
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            // 日時の新しいものから10件取得する
            // TakeLastは使えないため降順で10件取ってから、昇順に並べ替えている
            var flashcardContext = _context.Histories
                .Where(h => h.UserId == userId)
                .OrderByDescending(h => h.StudyDate)
                .Take(10);
            var HistoriesList = await flashcardContext.OrderBy(h => h.StudyDate).ToListAsync();

            // グラフデータ作成
            foreach (var item in HistoriesList)
            {
                if (!string.IsNullOrEmpty(viewModel.StudyDate))
                {
                    viewModel.StudyDate = viewModel.StudyDate + ", '" + item.StudyDate.ToString("yyyy/MM/dd") + "'";
                    viewModel.CorrectAnswerCount = viewModel.CorrectAnswerCount + ", " + item.CorrectAnswerCount.ToString();
                    viewModel.CorrectAnswerRate = viewModel.CorrectAnswerRate + ", " + ((double)item.CorrectAnswerCount / (double)item.StudyCount * 100).ToString();
                }
                else
                {
                    viewModel.StudyDate = "'" + item.StudyDate.ToString("yyyy/MM/dd") + "'";
                    viewModel.CorrectAnswerCount = item.CorrectAnswerCount.ToString();
                    viewModel.CorrectAnswerRate = ((double)item.CorrectAnswerCount / (double)item.StudyCount * 100).ToString();
                }
            }

            return View(viewModel);
        }

        // 履歴削除
        // POST: Histories/AllDelete
        [HttpPost, ActionName("AllDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AllDelete()
        {
            // ログインしていなければログイン画面へ
            if (_claim == null)
            {
                return RedirectToAction("Index", "Account");
            }

            // ログインユーザIDに紐づく履歴を取得する
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var histories = _context.Histories
                                    .Where(h => h.UserId == userId);
            if (histories != null)
            {
                // 削除する
                _context.Histories.RemoveRange(histories);
                await _context.SaveChangesAsync();
            }
            else
            {
                viewModel.ErrorMsg = "削除に失敗しました。";
                return RedirectToAction("Index", viewModel);
            }

            // 画面項目を初期化する
            viewModel.StudyDate = null;
            viewModel.CorrectAnswerCount = null;
            viewModel.CorrectAnswerRate = null;

            return RedirectToAction(nameof(Index), viewModel);
        }

        // POST: Histories/DownloadFile
        [HttpPost, ActionName("DownloadFile")]
        [ValidateAntiForgeryToken]
        public IActionResult DownloadFile()
        {
            // ログインしていなければログイン画面へ
            if (_claim == null)
            {
                return RedirectToAction("Index", "Account");
            }

            // ファイルに書き込むデータを取得
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var histories = _context.Histories
                                    .Where(h => h.UserId == userId)
                                    .ToList();
            // 出力項目編集

            // 項目名取得（GetFieldsで取得できないため手書き）
            string[] itemName = new string[] { "学習日", "出題数", "正答数" };

            string userName = User.Identity.Name;

            // ワークブックを新規作成
            using (var wb = new XLWorkbook())
            {
                // ワークシートを追加する
                wb.Worksheets.Add();
                // ワークシートを開く
                var ws = wb.Worksheet("Sheet1");
                // 先頭行（項目名）
                ws.FirstCell().InsertData(itemName);
                // ワークシートに履歴リストを挿入する
                ws.Cell("A2").InsertData(histories);

                // 出力
                using (MemoryStream stream = new MemoryStream())
                {
                    // ワークブックを保存する
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"histories_{userName}_{DateTime.Now.ToString("yyyyMMdd")}.xlsx");
                }
            }
        }
    }
}
