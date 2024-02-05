public class Suporte
{
    public static string nome { get; set; }
    public static string tipo { get; set; }
    public static int vida { get; set; }
    public static int forca { get; set; }

    public static void GetHeroi(string carta)
	{
        if (carta == "brutaniuz")
        {
            nome = carta;
            tipo = "ativo-passiva";
            vida = 15;
            forca = 3;
        }
        else if (carta == "tecnita")
        {
            nome = carta;
            tipo = "ativo-passiva";
            vida = 14;
            forca = 2;
        }
        else if (carta == "ariedam")
        {
            nome = carta;
            tipo = "ativo-passiva";
            vida = 16;
            forca = 1;
        }
        else if (carta == "menrart")
        {
            nome = carta;
            tipo = "ativo-passiva";
            vida = 14;
            forca = 3;
        }
    }

    public static void GetGenerica(string carta)
    {
        if (carta == "item1")
        {
            nome = carta;
            tipo = "ativo";
            forca = 1;
            vida = 4;
        }
        else if (carta == "item2")
        {
            nome = carta;
            tipo = "ativo";
            forca = 2;
            vida = 3;
        }
        else if (carta == "item3")
        {
            nome = carta;
            tipo = "ativo-passiva";
            forca = 1;
            vida = 2;
        }
        else if (carta == "item4")
        {
            nome = carta;
            tipo = "ativo-passiva";
            forca = 2;
            vida = 2;
        }
        else if (carta == "item5")
        {
            nome = carta;
            tipo = "passiva";
            forca = 3;
            vida = 2;
        }
    }
}


