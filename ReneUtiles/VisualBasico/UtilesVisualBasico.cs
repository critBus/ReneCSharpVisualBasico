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

using ReneUtiles.VisualBasico.Clases.Dialogos;

namespace ReneUtiles.VisualBasico
{
    public abstract class UtilesVisualBasico
    {
//Forte; 15.75pt; style=Bold
        //public static void getDlgInfo(string titulo, string mensaje)
        //{
        //    DialogoInformacion dlg = new DialogoInformacion(titulo,mensaje);
        //    dlg
        //}
        public static void showProgresDlg(Control c, string mensaje, metodoRealizar subproceso) {
            showProgresDlg(c, mensaje, subproceso, null);
        }
        public static void showProgresDlg(Control c, string mensaje, metodoRealizar subproceso, metodoRealizar accionEnVisualALTerminar)
        {

            //showDlgAceptarInf(mensaje);
            DialogoInformacion dlg  = new DialogoInformacion("Espere", mensaje);
            UtilesSubprocesos.subp(new auxiliarSubpYdespuesEnVisual(c,dlg, subproceso, accionEnVisualALTerminar)._subpYdespuesEnVisual);
            dlg.ShowDialog();
        }
        private class auxiliarSubpYdespuesEnVisual {
            Control c; metodoRealizar subproceso; metodoRealizar accionEnVisualALTerminar;
            //string mensaje;
            DialogoInformacion dlg;
            public auxiliarSubpYdespuesEnVisual(Control c, DialogoInformacion dlg, metodoRealizar subproceso, metodoRealizar accionEnVisualALTerminar)
            {
                this.c = c;
                this.subproceso = subproceso;
                this.accionEnVisualALTerminar = accionEnVisualALTerminar;
                this.dlg = dlg;
                //this.mensaje=mensaje;
               
            }
            public  void _subpYdespuesEnVisual()
            {
                try
                {
                    subproceso();
                    subpVisual(c, hidedlg);
                    if (accionEnVisualALTerminar!=null)
                    {
                        subpVisual(c, accionEnVisualALTerminar);
                    }
                   
                   
                }
                catch (Exception ex) {
                    subpVisual(c, hidedlg);
                    subpVisual(c, new auxiliarResponderException(ex).responderException);
                }
                
            }
            public void hidedlg() {
                dlg.Hide();
            }

            class auxiliarResponderException{
                Exception ex;
                public auxiliarResponderException(Exception ex) {
                    this.ex = ex;
                }
                public void  responderException(){
                    UtilesVisualBasico.reponderException(ex,"Error");
                }
            }

        }
        
        public static void subpVisual(Control c,metodoRealizar m) {
            c.Invoke(m);
        }
        public static FolderBrowserDialog showFolderBrowserDlg(metodoUtilizar<DirectoryInfo> accionOK) {
            FolderBrowserDialog bo = new FolderBrowserDialog();
            showFolderBrowserDlg(bo, accionOK);
            return bo;
        }
        public static void showFolderBrowserDlg(FolderBrowserDialog sf, metodoUtilizar<DirectoryInfo> accionOK)
        {
            DialogResult res = sf.ShowDialog();
            if (res == DialogResult.OK)
            {
                accionOK(new DirectoryInfo(sf.SelectedPath));
            }
        }
        public static OpenFileDialog showOpenFileDlgInit(metodoUtilizar<FileInfo> accionOK, string tituloDelDialogo, params string[] paresExplicacionExtencion) {
            
            OpenFileDialog of = new OpenFileDialog();
            initOpenFileDlg(of, tituloDelDialogo, paresExplicacionExtencion);
            showFileDlg(of, accionOK);
            return of;
        }

        public static SaveFileDialog  showSaveFileDlgInit(metodoUtilizar<FileInfo> accionOK,string tituloDelDialogo, params string[] paresExplicacionExtencion){
            SaveFileDialog sf = new SaveFileDialog();
            initFileDlg(sf, tituloDelDialogo, paresExplicacionExtencion);
            showFileDlg(sf, accionOK);

             
            //metodoUtilizar<FileInfo> accion = new metodoUtilizar<FileInfo>() ;

            return sf;
        }
        public static void initFileDlg(FileDialog sf,string tituloDelDialogo, params string[] paresExplicacionExtencion)
        {
            sf.AddExtension = true;
            addExtencionFileDlg(sf, paresExplicacionExtencion);
            sf.Title = tituloDelDialogo;
            sf.InitialDirectory = Directory.GetCurrentDirectory().ToString();
        }
        
        public static void initOpenFileDlg(OpenFileDialog sf, string tituloDelDialogo, params string[] paresExplicacionExtencion)
        {
            initFileDlg(sf, tituloDelDialogo, paresExplicacionExtencion);
            sf.Multiselect = false;
        }

        public static void showFileDlg(FileDialog sf,metodoUtilizar<FileInfo> accionOK) {
            DialogResult res = sf.ShowDialog();
            if (res == DialogResult.OK)
            {
                accionOK(new FileInfo(sf.FileName));
             }
        }
        //public static void showFileDlg(FileDialog sf, metodoUtilizar<FileInfo> accionOK)
        //{
        //    DialogResult res = sf.ShowDialog();
        //    if (res == DialogResult.OK)
        //    {
        //        accionOK(new FileInfo(sf.FileName));
        //    }
        //}
        public static void addExtencionFileDlgTxt(FileDialog fd) {
            addExtencionFileDlg(fd, "Archivo de Texto", ".txt", "Archivo de Texto", ".TXT");
        }
        public static void addExtencionFileDlg(FileDialog fd,params string[] paresExplicacionExtencion) {


            int length = paresExplicacionExtencion.Length;
            if (length % 2 == 0)
            {
                string extenciones = fd.Filter;
                for (int i = 0; i < length; i += 2)
                {
                    bool esPrimeraExtencion = String.IsNullOrWhiteSpace(extenciones);
                    if (esPrimeraExtencion)
                    {
                        fd.DefaultExt = paresExplicacionExtencion[i + 1];
                    }
                    extenciones += (esPrimeraExtencion ? "" : "|") + paresExplicacionExtencion[i] + "(*" + paresExplicacionExtencion[i + 1] + ")|*" + paresExplicacionExtencion[i + 1];
                    //Console.WriteLine(extenciones);
                }
                fd.Filter = extenciones;
            }

            
        }

        public static void reponderException(Exception ex,string mensaje) {
            Archivos.appenLogExeption(ex);
            showDlgAceptarError(mensaje);
        }
        public static void showDlgAceptarError(string mensaje)
        {
            MessageBox.Show(mensaje, "Error");
        }
        public static void showDlgAceptarInf(string mensaje) {
            
            MessageBox.Show(mensaje, "Informacion");
        }
        public static void showDlgAceptarAdvertencia(string mensaje)
        {
            MessageBox.Show(mensaje, "Advertencia");
        }

//        public static bool tieneRutaValidaArchivo(TextBox tb)
//        {
//            return Utiles.isEmptyFull(tb.Text) ? false : new FileInfo(tb.Text).Exists;
//        }
        
        public static bool tieneRutaValidaArchivo(TextBox tb)
        {
        	return Utiles.isEmptyFull(tb.Text) ? false :Archivos.existeArchivo(tb.Text);
        }
        
        public static FileInfo getFileInfo(TextBox tb)
        {
            return new FileInfo(tb.Text);
        }

//        public static bool tieneRutaValidaCarpeta(TextBox tb) {
//            return Utiles.isEmptyFull(tb.Text)?false:new DirectoryInfo(tb.Text).Exists;
//        }
		public static bool tieneRutaValidaCarpeta(TextBox tb) {
			return Utiles.isEmptyFull(tb.Text)?false:Archivos.existeCarpeta(tb.Text);
        }
        public static DirectoryInfo getDirectoryInfo(TextBox tb)
        {
            return new DirectoryInfo(tb.Text);
        }
        public static void setEnable(bool enable,params Control[] C){
            int total = C.Length;
            for (int i = 0; i < total; i++)
            {
                C[i].Enabled = enable;
            }
        }
    }
}
