/*
 * Created by SharpDevelop.
 * User: Rene
 * Date: 1/10/2021
 * Time: 13:23
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Windows.Forms;
using System.IO;
using System.Threading;
using ReneUtiles;

namespace ReneUtiles.VisualBasico
{
	/// <summary>
	/// Description of UtilesEventos.
	/// </summary>
	public abstract class UtilesEventos
	{
		public static void addOnClick(Control C,metodoRealizar r){
			C.Click+=(a,b)=>{r();};
		}
		
		public static void addOnClickShowProgresDlg(Control c, string mensajeCargando,string mensajeALTerminar, metodoRealizar subproceso){
			addOnClick(c,()=>{UtilesVisualBasico.showProgresDlg(c,mensajeCargando,subproceso,()=>{UtilesVisualBasico.showDlgAceptarInf(mensajeALTerminar);});});
		}
	}
}
