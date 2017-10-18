using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace DailyPlanning.Models.PagingModel
{
    public class Pager
    {
        public Pager(int totalItems, int? page,string controller,string action)
        {
            // calculate total, start and end pages
            var totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)PageSize);
            var currentPage = page != null ? (int)page : 1;
            var startPage = currentPage - 5;
            var endPage = currentPage + 4;
            if (startPage <= 0)
            {
                endPage -= (startPage - 1);
                startPage = 1;
            }
            if (endPage > totalPages)
            {
                endPage = totalPages;
                if (endPage > 10)
                {
                    startPage = endPage - 9;
                }
            }

            if (currentPage > endPage)
                currentPage = endPage;

            TotalItems = totalItems;
            CurrentPage = currentPage;
            //PageSize = pageSize;
            TotalPages = totalPages;
            StartPage = startPage;
            EndPage = endPage;
            Controller = controller;
            Action = action;
        }

        public int TotalItems { get; private set; }
        public int CurrentPage { get; private set; }
        public int PageSize { get { return int.Parse(WebConfigurationManager.AppSettings["ItemsShownPerPage"]); } }
        public int TotalPages { get; private set; }
        public int StartPage { get; private set; }
        public int EndPage { get; private set; }

        public string Controller { get; private set; }
        public string Action { get; private set; }
    }
}