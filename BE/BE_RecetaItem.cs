namespace BE
{
    // Un medicamento de la receta con su posologia.
    public class BE_RecetaItem
    {
        public int IdRecetaItem { get; set; }

        public string Medicamento { get; set; }

        public string Presentacion { get; set; }

        public string Posologia { get; set; }

        public int Cantidad { get; set; }
    }
}
