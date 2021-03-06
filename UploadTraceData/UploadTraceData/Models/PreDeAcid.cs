﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace UploadTraceData.Models
{
    //PreDeAcid
    public class PreDeAcid
    {

        /// <summary>
        /// hookId
        /// </summary>		

        public string hookId { get; set; }
        /// <summary>
        /// attachTime
        /// </summary>		

        public DateTime attachTime { get; set; }
        /// <summary>
        /// netWeight
        /// </summary>		

        //public decimal netWeight { get; set; }

        public decimal grossWeight { get; set; }
        public decimal hookWeight { get; set; }
        /// <summary>
        /// weighingTime
        /// </summary>		

        public DateTime weighingTime { get; set; }
        /// <summary>
        /// uploaded
        /// </summary>		

        public bool uploaded { get; set; }

    }
}