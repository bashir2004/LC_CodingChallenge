using CsvHelper.Configuration;
using LC_CodingChallenge.Helper;
using LC_CodingChallenge.Repository.Interfaces;
using LC_CodingChallenge.Repository.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace LC_CodingChallenge.Controllers
{
    public class LeaseController : Controller
    {
        private readonly IFileProvider fileProvider;
        private readonly ILeaseRepository leaseRepository;
        private IWebHostEnvironment hostEnvironment;
        public LeaseController(ILeaseRepository leaseRepository, IFileProvider fileProvider, IWebHostEnvironment hostEnvironment)
        {
            this.leaseRepository = leaseRepository;
            this.fileProvider = fileProvider;
            this.hostEnvironment = hostEnvironment;

        }
        [HttpGet]
        public IActionResult Index()
        {
            return View(new Lease());
        }
        [HttpPost]
        public IActionResult Index(Lease lease)
        {
            if (lease != null && lease.file != null)
            {
                var filePath = SaveFile.Save(lease.file, hostEnvironment);
                if (Path.GetExtension(filePath) == ".csv")
                {
                    var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                    {
                        IgnoreBlankLines = false,
                        HasHeaderRecord = true
                    };
                    var leases = Helper.CsvReader.Read<Lease>(config, filePath);
                    var datatable = LeaseModelToDataset.Convert(leases, lease.Logs);
                    if (lease.Logs.Count == 0)
                    {
                        int result = leaseRepository.BulkUpdateCSVFile(datatable);
                        if (result > 0)
                            lease.Logs.Add(1, "csv file upload successfully.");
                    }
                }
            }
            return View(lease);
        }
        public IActionResult ViewAll()
        {
            return View(leaseRepository.GetAllleases());
        }
    }
}
