using static System.Console;
using System;
using System.Diagnostics;
using System.Text;
using System.IO;
using System.Linq;


namespace MyProject
{
    class MainClass
    {
        public static void printMenu(String[] options)
        {
            foreach (var option in options)
            {
                WriteLine(option);

            }
            WriteLine("==================");
            WriteLine("Escolha uma opção:");

        }
        static void Main(string[] args)
        {
            String[] options = {
                "|> 1- Cadastrar <|",
                "|> 2- Editar    <|",
                "|> 3- Listar    <|",
                "|> 4- Excluir   <|",
                "|> 5- Localizar <|",
                "|> 6- Salvar    <|",
                "|> 7- Ler|      <|",
                "|> 8- Sair      <|"};
            int option = 0;
            while (true)
            {

                ForegroundColor = ConsoleColor.DarkYellow;
                WriteLine("==============================");
                WriteLine("|>>>>    MENU ESCOLAR    <<<<|");
                WriteLine("==============================");
                ResetColor();
                ForegroundColor = ConsoleColor.Yellow;
                printMenu(options);
                ResetColor();
                try
                {
                    option = Convert.ToInt32(ReadLine());
                }
                catch (FormatException)
                {
                    ForegroundColor = ConsoleColor.Red;
                    WriteLine($"Digite uma opção entre 1 e {options.Length}");
                    ResetColor();
                    continue;
                }
                catch (Exception)
                {
                    ForegroundColor = ConsoleColor.Red;
                    WriteLine("Ocorreu um erro, tente novamente.");
                    ResetColor();
                    continue;
                }
                switch (option)
                {
                    case 1:
                        Cadastrar();
                        break;
                    case 2:
                        Editar();
                        break;
                    case 3:
                        Listar();
                        break;
                    case 4:
                        Excluir();
                        break;
                    case 5:
                        Localizar();
                        break;
                    case 6:
                        Salvar();
                        break;
                    case 7:
                        Ler();
                        break;
                    case 8:
                        Environment.Exit(0);
                        break;
                    default:
                        ForegroundColor = ConsoleColor.Red;
                        WriteLine($"Digite um numero de 1 a {options.Length}");
                        ResetColor();
                        break;
                }
            }
        }
        static List<string> Alunos = new List<string>();
        static List<string> Aprovados = new List<string>();
        static List<string> Recuperacao = new List<string>();
        static List<string> Reprovados = new List<string>();
        static List<double> Medias = new List<double>();
        static List<double> Notas = new List<double>();


        static double media = 0;
        private static void Cadastrar()
        {
            Clear();
            ForegroundColor = ConsoleColor.DarkYellow;
            WriteLine("==============================");
            WriteLine("|>>>> CADASTRO DE ALUNOS <<<<|");
            WriteLine("==============================");
            ResetColor();
            ForegroundColor = ConsoleColor.Yellow;
            string nome = "";
            WriteLine("Digite o nome do aluno: ");
            ResetColor();
            nome = ReadLine();
            var repetido = Alunos.Any(x => x.Contains(nome));

            if (repetido == true)
            {
                ForegroundColor = ConsoleColor.Red;
                WriteLine("Esse aluno já está cadastrado!!");
                ResetColor();
                return;
            }
            else
            {
                Alunos.Add(nome);

            }
            media = 0;
            for (int i = 0; i < 4; i++)
            {
                ForegroundColor = ConsoleColor.Yellow;
                WriteLine($"|Digite a {i + 1}° nota: ");
                ResetColor();
                double nota = Convert.ToDouble(ReadLine());
                Notas.Add(nota);
                media = media + nota;
            }
            media = media / 4;
            Medias.Add(media);
            if (media >= 7)
            {
                Aprovados.Add(nome);
            }
            else if (media < 7 && media >= 5)
            {
                Recuperacao.Add(nome);
            }
            else
            {
                Reprovados.Add(nome);
            }
        }
        private static void Editar()
        {
            Clear();
            ForegroundColor = ConsoleColor.DarkYellow;
            WriteLine("===============================");
            WriteLine("|>>>> EDIÇÂO DE CADASTROS <<<<|");
            WriteLine("===============================");
            ResetColor();
            ForegroundColor = ConsoleColor.Yellow;
            WriteLine("Digite o nome: ");
            string nome = ReadLine();
            
            int index = Alunos.IndexOf(nome);

            if (index >= 0)
            {
                ForegroundColor = ConsoleColor.Yellow;
                WriteLine("=========================");
                WriteLine($"|Cadastro que sera editado:\n" +
                          $"|Aluno: {nome}\n" +
                          $"|1° Nota: {Notas[index * 4]}\n" +
                          $"|2° Nota: {Notas[index * 4 + 1]}\n" +
                          $"|3° Nota: {Notas[index * 4 + 2]}\n" +
                          $"|4° Nota: {Notas[index * 4 + 3]}\n" +
                          $"|Media: {Medias[index].ToString("F")}\n");
                WriteLine("=========================");
                Alunos.RemoveAt(index);
                Medias.RemoveAt(index);
                Notas.RemoveRange(index * 4, 4);
                Aprovados.Remove(nome);
                Reprovados.Remove(nome);
                Recuperacao.Remove(nome);
                WriteLine("Digite o novo nome: ");
                Alunos.Add(ReadLine());

                double somaNotas = 0;
                for (int i = index * 4; i < index * 4 + 4; i++)
                {
                    WriteLine($"Digite a nova nota {i - index * 4 + 1}: ");
                    Notas.Add(Convert.ToDouble(ReadLine()));
                    somaNotas += Notas[i];
                }
                double media = somaNotas / 4.0;
                Medias.Add(media);

                if (media >= 7)
                {
                    Aprovados.Add(Alunos[index]);
                }
                else if (media < 7 && media >= 5)
                {
                    Recuperacao.Add(Alunos[index]);
                }
                else
                {
                    Reprovados.Add(Alunos[index]);
                }

                ResetColor();
                ForegroundColor = ConsoleColor.Green;
                WriteLine("======================================");
                WriteLine(">>>> CADASTRO EDITADO COM SUCESSO <<<<");
                WriteLine("======================================");
                ResetColor();
            }
            else
            {
                ForegroundColor = ConsoleColor.Red;
                WriteLine("Cadastro nao encontrado!!");
                ResetColor();
            }
        }

        private static void Listar()
        {
            Clear();
            ForegroundColor = ConsoleColor.DarkYellow;
            WriteLine("===============================");
            WriteLine("|>>>> LISTA DE CADASTROS  <<<<|");
            WriteLine("===============================");
            ResetColor();
            ForegroundColor = ConsoleColor.Yellow;
            string situacao = "";
            for (int i = 0; i < Alunos.Count; i++)
            {

                if (Aprovados.Contains(Alunos[i]) == true)
                {
                    situacao = "Aprovado";
                }
                else if (Recuperacao.Contains(Alunos[i]) == true)
                {
                    situacao = "Recuperação";
                }
                else if (Reprovados.Contains(Alunos[i]) == true)
                {
                    situacao = "Reprovado";
                }
                WriteLine($"||Nome: {Alunos[i]} " +
               $"|| Media: {Medias[i].ToString("F")}" +
               $"|| Situação: {situacao}");

                for (int j = i * 4; j < i * 4 + 4; j++)
                {
                    WriteLine($"|| Nota: {Notas[j]}");
                }
                WriteLine("=========================");
            }
            ForegroundColor = ConsoleColor.Yellow;
            WriteLine($"||Total de alunos aprovados: {Aprovados.Count}");
            WriteLine($"||Lista de alunos aprovados: ");

            for (int x = 0; x < Aprovados.Count; x++)
            {
                WriteLine($"||Aluno: {Aprovados[x]}");
            }
            WriteLine("===============================");
            WriteLine($"||Total de alunos em recuperação: {Recuperacao.Count}");
            WriteLine($"||Lista de alunos em recuperação: ");

            for (int y = 0; y < Recuperacao.Count; y++)
            {
                WriteLine($"||Aluno: {Recuperacao[y]}");
            }
            WriteLine("===============================");
            WriteLine($"||Total de alunos reprovados: {Reprovados.Count}");
            WriteLine($"||Lista de alunos reprovados: ");
            ResetColor();
            for (int z = 0; z < Reprovados.Count; z++)
            {
                WriteLine($"||Aluno: {Reprovados[z]}");
            }
            double mediatotal = 0;
            for (int i = 0; i < Medias.Count; i++)
            {
                mediatotal = mediatotal + Medias[i];
            }
            mediatotal = mediatotal / Alunos.Count;
            ForegroundColor = ConsoleColor.Yellow;
            WriteLine("===============================");
            WriteLine($"MEDIA TOTAL DA TURMA: {mediatotal.ToString("F")}");
            WriteLine("===============================\n\n");
            ResetColor();
        }
        private static void Excluir()
        {
            Clear();
            ForegroundColor = ConsoleColor.DarkYellow;
            WriteLine("===============================");
            WriteLine("|>>>>  EXCLUIR CADASTRO   <<<<|");
            WriteLine("===============================");
            ResetColor();
            ForegroundColor = ConsoleColor.Yellow;
            string nome = "";
            WriteLine("Digite o nome: ");

            nome = ReadLine();

            int index = Alunos.IndexOf(nome);
            if (index >= 0)
            {
                // Mostra o cadastro selecionado
                WriteLine("=========================");
                WriteLine($"Cadastro selecionado: {Alunos[index]}\n" +
                          $"Media: {Medias[index]}");
                for (int j = index * 4; j < (index + 1) * 4; j++)
                {
                    WriteLine($"Nota {j % 4 + 1}: {Notas[j]}");
                }
                WriteLine("=========================");

                // confirmação para excluir o cadastro
                WriteLine("Tem certeza que deseja excluir esse cadastro? (s/n)");

                string resposta = ReadLine();
                if (resposta.ToLower() == "s")
                {
                    // nessa codicional sera removido o cadastro e as notas do aluno
                    Alunos.RemoveAt(index);
                    Medias.RemoveAt(index);
                    Notas.RemoveRange(index * 4, 4);
                    Aprovados.Remove(nome);
                    Reprovados.Remove(nome);
                    Recuperacao.Remove(nome);
                    ResetColor();
                    ForegroundColor = ConsoleColor.Green;
                    WriteLine("=================================");
                    WriteLine("|>Cadastro excluído com sucesso<|");
                    WriteLine("=================================");
                    ResetColor();
                }
                else
                {
                    WriteLine("Cadastro não excluído.");
                }
            }
            else
            {
                ForegroundColor = ConsoleColor.Red;
                WriteLine($"Não foi encontrado nenhum cadastro com o nome {nome}.");
                ResetColor();
            }
        }

        private static void Localizar()
        {
            Clear();
            ForegroundColor = ConsoleColor.DarkYellow;
            WriteLine("===============================");
            WriteLine("|>>>> LOCALIZAR CADASTROS <<<<|");
            WriteLine("===============================");
            ResetColor();
            ForegroundColor = ConsoleColor.Yellow;
            WriteLine("Digite o nome do aluno a ser localizado: ");
            string nome = ReadLine();
            int index = Alunos.IndexOf(nome);
            if (index >= 0)
            {
                ForegroundColor = ConsoleColor.Green;
                WriteLine("===================================");
                WriteLine("|>CADASTRO LOCALIZADO COM SUCESSO<|");
                WriteLine("===================================");
                ResetColor();
                ForegroundColor = ConsoleColor.Yellow;
                WriteLine("=========================");
                WriteLine($"|Aluno: {nome}\n" +
                          $"|1° Nota: {Notas[index * 4]}\n" +
                          $"|2° Nota: {Notas[index * 4 + 1]}\n" +
                          $"|3° Nota: {Notas[index * 4 + 2]}\n" +
                          $"|4° Nota: {Notas[index * 4 + 3]}\n" +
                          $"|Media: {Medias[index].ToString("F")}\n");
                WriteLine("=========================");
                ResetColor();

            }
            else
            {
                ForegroundColor = ConsoleColor.Red;
                WriteLine("Aluno não encontrado! Por favor, tente novamente.");
                ResetColor();
            }
        }
        static bool erro = true;
        private static void Salvar()
        {
            ForegroundColor = ConsoleColor.DarkYellow;
            WriteLine("=============================");
            WriteLine(">>>>  GRAVANDO OS DADOS  <<<<");
            WriteLine("|      ..Processando..      |");
            WriteLine("=============================\n");
            ResetColor();
            try
            {
                StreamWriter aluno;
                string arq = @"C:\Users\Aluno\source\repos\escoleta\aluno.txt";
                aluno = File.CreateText(arq);
                foreach (var item in Alunos)
                {
                    aluno.WriteLine($"{item}");
                }
                aluno.Close();
                StreamWriter nota;
                string arq2 = @"C:\Users\Aluno\source\repos\escoleta\nota.txt";
                nota = File.CreateText(arq2);
                foreach (var item2 in Notas)
                {
                    nota.WriteLine($"{item2}");

                }
                nota.Close();
                StreamWriter medias;
                string arq3 = @"C:\Users\Aluno\source\repos\escoleta\medias.txt";
                medias = File.CreateText(arq3);
                foreach (var item3 in Medias)
                {
                    medias.WriteLine($"{item3}");
                }
                medias.Close();
                StreamWriter apv;
                string arq4 = @"C:\Users\Aluno\source\repos\escoleta\apv.txt";
                apv = File.CreateText(arq4);
                foreach (var item4 in Aprovados)
                {
                    apv.WriteLine($"{item4}");
                }
                apv.Close();
                StreamWriter rec;
                string arq5 = @"C:\Users\Aluno\source\repos\escoleta\rec.txt";
                rec = File.CreateText(arq5);
                foreach (var item5 in Recuperacao)
                {
                    rec.WriteLine($"{item5}");
                }
                rec.Close();
                StreamWriter rep;
                string arq6 = @"C:\Users\Aluno\source\repos\escoleta\rep.txt";
                rep = File.CreateText(arq6);
                foreach (var item6 in Reprovados)
                {
                    rep.WriteLine($"{item6}");
                }
                rep.Close();
                erro = false;
            }
            catch (Exception e)
            {
                
                ForegroundColor = ConsoleColor.Red;
                WriteLine($"Erro: {e.Message}");
                ResetColor();
                
            }
            finally
            {
                if (!erro)
                {
                    ForegroundColor = ConsoleColor.Green;
                    WriteLine("================================");
                    WriteLine("|> DADOS GRAVADOS COM SUCESSO <|");
                    WriteLine("================================\n\n");
                    ResetColor();
                }
            }


        }

        private static void Ler()
        {
            Clear();
            ForegroundColor = ConsoleColor.DarkYellow;
            WriteLine("========================================");
            WriteLine("|          >>>> LER DADOS <<<<         |");
            WriteLine("========================================");
            ResetColor();

            var aluno = File.ReadAllLines(@"C:\Users\Aluno\source\repos\escoleta\aluno.txt");
            for (int i = 0; i < aluno.Length; i++)
            {
                Alunos.Add(aluno[i]);
            }

            var nota = File.ReadAllLines(@"C:\Users\Aluno\source\repos\escoleta\nota.txt");
            for (int x = 0; x < nota.Length; x++)
            {
                Notas.Add(Convert.ToDouble(nota[x]));
            }

            var medias = File.ReadAllLines(@"C:\Users\Aluno\source\repos\escoleta\medias.txt");
            for (int i = 0; i < medias.Length; i++)
            {
                Medias.Add(Convert.ToDouble(medias[i]));
            }

            var apv = File.ReadAllLines(@"C:\Users\Aluno\source\repos\escoleta\apv.txt");
            for (int i = 0; i < apv.Length; i++)
            {
                Aprovados.Add(apv[i]);
            }

            var rec = File.ReadAllLines(@"C:\Users\Aluno\source\repos\escoleta\rec.txt");
            for (int i = 0; i < rec.Length; i++)
            {
                Recuperacao.Add(rec[i]);
            }

            var rep = File.ReadAllLines(@"C:\Users\Aluno\source\repos\escoleta\rep.txt");
            for (int i = 0; i < rep.Length; i++)
            {
                Reprovados.Add(rep[i]);
            }
            ForegroundColor = ConsoleColor.Green;
            WriteLine("========================================");
            WriteLine("|>>>>  LEITURA DE DADOS CONCLUIDA<<<<  |");
            WriteLine("========================================\n\n");
            ResetColor();
        }

    }
}