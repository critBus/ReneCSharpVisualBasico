/*
 * Created by SharpDevelop.
 * User: Rene
 * Date: 1/10/2021
 * Time: 12:21
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
	/// Description of UtilesValidacion.
	/// </summary>
	public abstract class UtilesValidacion
	{
		
		
		public static metodoRealizar setValidacionURLsCarpetas(TextBox[] TB, Button[] B,params metodoCreador<bool>[] condicionesExtrasParaActivarLosBotones)
		{
		
			Predicate<TextBox> p = (tb) => {
				return UtilesVisualBasico.tieneRutaValidaCarpeta(tb);
			};
			metodoRealizar activarODesactivarB = () => {
				bool valido = true;
				for (int i = 0; i < TB.Length; i++) {
					if ((!p(TB[i]))) {
						valido = false;
						break;
					}
					
				}
				if (condicionesExtrasParaActivarLosBotones != null) {
					for (int j = 0; j < condicionesExtrasParaActivarLosBotones.Length; j++) {
						if ((!condicionesExtrasParaActivarLosBotones[j]())) {
							valido = false;
							break;
						}	
					}
				}
				
				UtilesVisualBasico.setEnable(valido, B);
			};
			
			for (int i = 0; i < TB.Length; i++) {
				TextBox tb = TB[i];
				TB[i].TextChanged += (a, b) => {
					tb.ForeColor = p(tb) ? Color.Green : Color.Red;
					activarODesactivarB();
				};
			}
			
			return activarODesactivarB;
			
		}
		
//		public static metodoRealizar setValidacionURLsCarpetas(TextBox[] TB, params Button[] B)
//		{
//			
//		}
	}
}
