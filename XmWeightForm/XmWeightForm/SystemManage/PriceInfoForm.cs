﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AppService;
using AppService.Model;
using Dapper_NET20;
using DevComponents.DotNetBar;

namespace XmWeightForm.SystemManage
{
    public partial class PriceInfoForm : Office2007Form
    {
        public PriceInfoForm()
        {
            InitializeComponent();
            this.EnableGlass = false;
        }

        public int PriceKeyId = 0;
        public PriceInfoForm(int priceKeyId)
        {
            InitializeComponent();
            this.EnableGlass = false;
            PriceKeyId = priceKeyId;

            InitEditData(priceKeyId);
        }


        private void InitEditData(int id)
        {
            var model=new AnimalTypes();
            using (var db = DapperDao.GetInstance())
            {
                model = db.Query<AnimalTypes>("select * from AnimalTypes where animalTypeId=@id", new { id = id }).FirstOrDefault();
            }

            if (model != null)
            {
                animalName.Text = model.animalTypeName;
                txtPrice.Text = model.price.ToString();
                txttraceCode.Text = model.traceCode;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var name = animalName.Text.Trim();
            var price = txtPrice.Text.Trim();
            string traceCode = txttraceCode.Text.Trim();
            decimal priceDecimal = 0;
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("动物类型不能为空");
                return;
            }
            if (string.IsNullOrEmpty(traceCode))
            {
                traceCode = "01";
                //MessageBox.Show("溯源编号不能为空");
                //return;
            }
            if (traceCode.Length != 2)
            {
                MessageBox.Show("溯源编号长度为两位");
                return;
            }
            if (!string.IsNullOrEmpty(price))
            {

                try
                {
                    priceDecimal=decimal.Parse(price);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("价格数据格式不正确");
                    return;
                }
                //if (!ValidaterHelper.IsNumber(price))
                //{
                //    MessageBox.Show("价格数据格式不正确");
                //    return;
                    
                //}
            }
            try
            {
                int resultNum = 0;
                if (PriceKeyId == 0)
                {
                    string sql = "insert into AnimalTypes(animalTypeName,price,traceCode)values(@animalTypeName,@price,@traceCode)";
                    using (var db = DapperDao.GetInstance())
                    {
                        resultNum = db.Execute(sql, new { animalTypeName = name, price = priceDecimal, traceCode = traceCode });
                    }
                }
                else
                {
                    string sql = "update AnimalTypes set animalTypeName=@animalTypeName,price=@price,traceCode=@traceCode where animalTypeId=" +
                                 PriceKeyId;

                    using (var db = DapperDao.GetInstance())
                    {
                        resultNum = db.Execute(sql, new { animalTypeName = name, price = priceDecimal, traceCode = traceCode });
                    }
                }

                if (resultNum > 0)
                {
                    MessageBox.Show("保存成功");
                    this.DialogResult = DialogResult.OK;
                   
                }
                else
                {
                    MessageBox.Show("保存失败");
                }

                this.Close();
            }
            catch (Exception ex)
            {
               log4netHelper.Exception(ex);
                MessageBox.Show("保存失败");
            }
           
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
