﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using System.Data;
using System.Windows.Forms;
using AppService.Model;

namespace XmWeightForm.log
{
    public class SQLiteHelper
    {
        private SQLiteHelper()
        {

        }
        private static string dbPath = "Data Source=" + Application.StartupPath + @"\db\HookData.db";

        public static int ExecuteNonQuery(string sql, SQLiteParameter[] parameters)
        {
            int result = 0;
            SQLiteConnection cn = new SQLiteConnection(dbPath);
            SQLiteCommand cmd = cn.CreateCommand();
            try
            {
                cmd.CommandText = sql;

                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters);
                }
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                log4netHelper.Exception(ex);
            }
            finally
            {
                cmd.Dispose();
                cn.Close();
            }
            return result;

        }

        public static object ExecuteScalar(string sql, SQLiteParameter[] parameters)
        {
            object result = null;
            SQLiteConnection cn = new SQLiteConnection(dbPath);
            SQLiteCommand cmd = cn.CreateCommand();
            try
            {
                cmd.CommandText = sql;

                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters);
                }
                result = cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                log4netHelper.Exception(ex);
            }
            finally
            {
                cmd.Dispose();
                cn.Close();
            }



            return result;
        }

        /// <summary>
        /// Shortcut method to execute dataset from SQL Statement and object[] arrray of  parameter values
        /// </summary>
        /// <param name="cn">Connection.</param>
        /// <param name="commandText">Command text.</param>
        /// <param name="paramList">Param list.</param>
        /// <returns></returns>
        public static DataSet ExecuteDataSet(string sql, SQLiteParameter[] parameters)
        {
            DataSet ds = new DataSet();
            SQLiteConnection cn = new SQLiteConnection(dbPath);
            SQLiteCommand cmd = cn.CreateCommand();
            try
            {
                cmd.CommandText = sql;

                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters);
                }
                SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                log4netHelper.Exception(ex);
            }
            finally
            {
                cmd.Dispose();
                cn.Close();
            }
            return ds;
        }

        public static List<string> ExcuteHookList(string sql, SQLiteParameter[] parameters)
        {
            List<string> hooks = new List<string>();
            SQLiteConnection cn = new SQLiteConnection(dbPath);
            SQLiteCommand cmd = cn.CreateCommand();
            try
            {
                cmd.CommandText = sql;

                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters);
                }
                SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0)
                {
                    var tb=ds.Tables[0];
                    int rowCount=ds.Tables[0].Rows.Count;
                    if ( rowCount> 0)
                    {
                        for (int i = 0; i <rowCount; i++)
                        {
                            string hookrow=tb.Rows[i]["hookId"].ToString();
                            hooks.Add(hookrow);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log4netHelper.Exception(ex);
            }
            finally
            {
                cmd.Dispose();
                cn.Close();
            }
            return hooks;
        }
        public static tempWeight ExcuteTempWeightModel(string sql, SQLiteParameter[] parameters)
        {
            var model = new tempWeight();
            SQLiteConnection cn = new SQLiteConnection(dbPath);
            SQLiteCommand cmd = cn.CreateCommand();
            try
            {
                cmd.CommandText = sql;

                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters);
                }
                SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0)
                {
                    var tb = ds.Tables[0];
                    int rowCount = ds.Tables[0].Rows.Count;
                    if (rowCount > 0)
                    {
                        model.Id =tb.Rows[0]["Id"].ToString();
                        model.batchId = tb.Rows[0]["batchId"].ToString();
                        model.weight = tb.Rows[0]["weight"].ToString();
                        model.productName = tb.Rows[0]["productName"].ToString();
                        model.productPrice = tb.Rows[0]["productPrice"].ToString();
                        model.hookCount = int.Parse(tb.Rows[0]["hookCount"].ToString());
                        model.timespan = int.Parse(tb.Rows[0]["timespan"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                log4netHelper.Exception(ex);
            }
            finally
            {
                cmd.Dispose();
                cn.Close();
            }
            return model;
        }
        public static List<tempWeight> ExcuteTempWeightList(string sql, SQLiteParameter[] parameters)
        {
            List<tempWeight> tempWeight = new List<tempWeight>();
            SQLiteConnection cn = new SQLiteConnection(dbPath);
            SQLiteCommand cmd = cn.CreateCommand();
            try
            {
                cmd.CommandText = sql;

                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters);
                }
                SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0)
                {
                    var tb = ds.Tables[0];
                    int rowCount = ds.Tables[0].Rows.Count;
                    if (rowCount > 0)
                    {
                        for (int i = 0; i < rowCount; i++)
                        {
                            var model=new tempWeight();
                            model.Id = tb.Rows[0]["Id"].ToString();
                            model.batchId= tb.Rows[i]["batchId"].ToString();
                            model.weight=tb.Rows[i]["weight"].ToString();
                            model.productName = tb.Rows[i]["productName"].ToString();
                            model.productPrice = tb.Rows[i]["productPrice"].ToString();
                            model.hookCount = int.Parse(tb.Rows[i]["hookCount"].ToString());
                            model.timespan = int.Parse(tb.Rows[i]["timespan"].ToString());
                            tempWeight.Add(model);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log4netHelper.Exception(ex);
            }
            finally
            {
                cmd.Dispose();
                cn.Close();
            }
            return tempWeight;
        }
    }
}
