using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.IO;


using ReneUtiles;
using ReneUtiles.VisualBasico;
namespace ReneUtiles.VisualBasico.Clases.Administrador
{
	/// <summary>
	/// Description of SistemaEA.
	/// </summary>
	public class SistemaEA<E>
	{
		public string NombreEA_ConExtencion;
		public metodoCreador<E> CreadorEA;
		public metodoUtilizar<E> UtilizarEA;
		public SistemaEA(string nombreEA_ConExtencion
		                 ,metodoCreador<E> creadorEA
		                 ,metodoUtilizar<E> utilizarEA)
		{
			this.NombreEA_ConExtencion=nombreEA_ConExtencion;
			this.CreadorEA=creadorEA;
			this.UtilizarEA=utilizarEA;
		}
		
		private string getUrlEA()
		{
			return Directory.GetCurrentDirectory().ToString() + "/" + NombreEA_ConExtencion;
		}
		
		public void crearEA()
		{
			Archivos.saveObject(getUrlEA(), CreadorEA());
		
		}
		
		
		public void cargarEA()
		{
			try {
				try {
					string url = getUrlEA();
					if (Archivos.existeArchivo(url)) {
						E ea = (E)Archivos.readObject(url);
						UtilizarEA(ea);
						//algo..
					} else {
						crearEA();
					}
				} catch (Exception ex) {
					UtilesVisualBasico.reponderException(ex, "Error al cargar");
					crearEA();
				}
			} catch (Exception ex) {
				UtilesVisualBasico.reponderException(ex, "Error");
			}
		}

	}
}
