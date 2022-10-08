using _AspNet.Entities;
using System.Globalization;

namespace _AspNet
{
    class program
    {
        static void Main(string[] args)
        {

            Console.Write("Caminho do arquivo: ");
            string path = Console.ReadLine();

            List<Products> list = new();

            using (StreamReader sr = File.OpenText(path))
            {
                while (!sr.EndOfStream)
                {
                    string[] filds = sr.ReadLine().Split(',');
                    string name = filds[0];
                    double price = double.Parse(filds[1], CultureInfo.InvariantCulture);

                    list.Add(new (name, price));
                }
            }

            //se a lista for vazia, atribuir o valor 0
            var avg = list
                .Select(p => p.Price)
                .DefaultIfEmpty(0.0)
                .Average();

            Console.Write($"Preço medio dos produtos do arquivo: {avg.ToString("f2", CultureInfo.InvariantCulture)}");

           
            Console.WriteLine("Produtos com preços menores que o valor da média:");
            var names =
                list.Where(p => p.Price < avg)
                .OrderByDescending(p => p.Name)
                .Select(p => p.Name);

            foreach(string name in names)
            {
                
                Console.WriteLine(name);
            }
        }
    }
}
