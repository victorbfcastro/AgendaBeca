using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Linq;

namespace AgendaBeca
{
    class Agenda
    {
        protected static List<Contato> agenda = new List<Contato>();
        static readonly string caminho = @"c:\temp\agenda\agenda.txt";

        private static void SalvarAgenda()
        {
            try
            {
                using (StreamWriter sw = File.AppendText(caminho))
                {
                    for (int i = 0; i < agenda.Count; i++)
                    {
                        sw.WriteLine($"{agenda[i].Id}, {agenda[i].Nome}, {agenda[i].Telefone}, {agenda[i].Email}");
                    }
                }
            }

            catch (IOException e)
            {
                Console.WriteLine($"Erro! - {e.Message}");
            }

        }

        private static void EditarContato(int indexcontato)
        {
            int indexContato = indexcontato;

            Console.WriteLine($"\nQual dado gostaria de alterar do contato '{agenda[indexContato].Nome}' ?");
            Console.WriteLine("\n[1] Nome\n" +
                                "[2] Telefone\n" +
                                "[3] Email\n");
            Console.Write("> ");
            int opcao = int.Parse(Console.ReadLine());

            switch (opcao)
            {
                case 1: //Alterar nome
                    {
                        while (true)
                        {
                            Console.Write("Informe um novo Nome: ");
                            string novoNome = Console.ReadLine();

                            Console.Write($"[Aviso] O contato '{agenda[indexContato].Nome}' terá seu NOME alterado para '{novoNome}'\n" +
                                           "Confirma (S/N)? ");
                            string resposta = Console.ReadLine().ToUpper();
                            while (resposta.ToUpper() != "S" && resposta.ToUpper() != "N")
                            {
                                Console.Write("Confirme com 'S' ou 'N' para cancelar" +
                                              "Confirma (S/N)? ");
                                resposta = Console.ReadLine().ToUpper();
                            }
                            if (resposta.ToUpper() == "S")
                            {
                                agenda[indexContato].Nome = novoNome.ToUpper();
                                Console.WriteLine("\n> Nome do contato atualizado com sucesso!");
                                SalvarAgenda();
                                break;
                            }
                            else if (resposta == "N")
                            {
                                Console.WriteLine("\n> Alteração de nome cancelada!");
                                break;
                            }

                            break;
                        }
                        break;
                    }
                case 2: //Alterar telefone
                    {
                        while (true)
                        {
                            Console.Write("Informe um novo Telefone: ");
                            string novoTelefone = Console.ReadLine();

                            Console.Write($"[Aviso] O contato '{agenda[indexContato].Nome}' terá seu TELEFONE alterado para '{novoTelefone}'\n" +
                                           "Confirma (S/N)? ");
                            string resposta = Console.ReadLine().ToUpper();
                            while (resposta.ToUpper() != "S" && resposta.ToUpper() != "N")
                            {
                                Console.Write("Confirme com 'S' ou 'N' para cancelar" +
                                              "Confirma (S/N)? ");
                                resposta = Console.ReadLine().ToUpper();
                            }
                            if (resposta.ToUpper() == "S")
                            {
                                agenda[indexContato].Telefone = novoTelefone;
                                Console.WriteLine("\n> Telefone do contato atualizado com sucesso!");
                                SalvarAgenda();
                                break;
                            }
                            else if (resposta == "N")
                            {
                                Console.WriteLine("\n> Alteração de telefone cancelada!");
                                break;
                            }

                            break;
                        }
                        break;
                    }
                case 3: //Alterar Email
                    {
                        while (true)
                        {
                            Console.Write("Informe um novo E-mail: ");
                            string novoEmail = Console.ReadLine();

                            Console.Write($"[Aviso] O contato '{agenda[indexContato].Nome}' terá seu E-MAIL alterado para '{novoEmail}'\n" +
                                           "Confirma (S/N)? ");
                            string resposta = Console.ReadLine().ToUpper();
                            while (resposta.ToUpper() != "S" && resposta.ToUpper() != "N")
                            {
                                Console.Write("Confirme com 'S' ou 'N' para cancelar" +
                                              "Confirma (S/N)? ");
                                resposta = Console.ReadLine().ToUpper();
                            }
                            if (resposta.ToUpper() == "S")
                            {
                                agenda[indexContato].Email = novoEmail;
                                Console.WriteLine("\n> E-mail do contato atualizado com sucesso!");
                                SalvarAgenda();
                                break;
                            }
                            else if (resposta == "N")
                            {
                                Console.WriteLine("\n> Alteração de e-mail cancelada!");
                                break;
                            }

                            break;
                        }
                        break;
                    }
            }

        }

        private static void MostrarAgenda()
        {
            if (agenda.Count == 0)
            {
                Console.WriteLine("> Agenda está vazia! Adicione contatos.");
            }
            else
            {
                for (int i = 0; i < agenda.Count; i++)
                {
                    Console.WriteLine($"#{agenda.IndexOf(agenda[i])} - Nome: {agenda[i].Nome} | Telefone: {agenda[i].Telefone} | E-mail: {agenda[i].Email}");
                }
            }

            Console.WriteLine();
        }

        private static void BuscarContato(int indexcontato)
        {
            try
            {
                Console.WriteLine($"\n#{agenda[indexcontato].Id} - Nome: {agenda[indexcontato].Nome} | Telefone: {agenda[indexcontato].Telefone} | E-mail: {agenda[indexcontato].Email}\n");
            }
            catch (Exception)
            {
                Console.WriteLine("> Erro! Não foi encontrado nenhum contato com essa informação.");
            }
        }

        private static int RetornaIndex(string infocontato)    // Retorna o Index do contato buscado
        {
            try
            {
                if (infocontato.Contains("@"))  // verifica se o parametro de busca passado é um email
                {
                    return agenda.FindIndex(contato => contato.Email == infocontato);
                }
                if (double.TryParse(infocontato, out double verifica))  //verifica se o parametro  de busca passado é um número de telefone ou nome
                {
                    return agenda.FindIndex(contato => contato.Telefone == infocontato);
                }
                else
                {
                    return agenda.FindIndex(contato => contato.Nome == infocontato.ToUpper());
                }
            }
            catch (Exception)
            {
                Console.WriteLine("\n> não foi possível localizar o contato pela informação passada.");
                return -1;
            }
        }

        private static void AdicionarContato(string nome, string telefone, string email)
        {
            Contato contato = new Contato(nome, telefone, email);
            agenda.Add(contato);
            Console.WriteLine($"\n> Novo contato '{nome}' adicionado com sucesso!\n");
            contato.Id = (agenda.IndexOf(contato)).ToString();

            SalvarAgenda();
        }

        private static void RemoverContato(int indexcontato)
        {
            while (true)
            {
                Console.Write($"\n[Aviso] O contato '{agenda[indexcontato].Nome}' será REMOVIDO.\n" +
                               "Confirma (S/N)? ");
                string resposta = Console.ReadLine().ToUpper();
                while (resposta.ToUpper() != "S" && resposta.ToUpper() != "N")
                {
                    Console.Write("Confirme com 'S' ou 'N' para cancelar" +
                                  "Confirma (S/N)? ");
                    resposta = Console.ReadLine().ToUpper();
                }
                if (resposta.ToUpper() == "S")
                {
                    Console.WriteLine($"\n> Contato '{agenda[indexcontato].Nome}' removido com sucesso!\n");
                    var contato = agenda.Single(x => x.Id == indexcontato.ToString());
                    agenda.Remove(contato);
                    SalvarAgenda();
                    break;
                }
                else if (resposta == "N")
                {
                    Console.WriteLine("\n> Remoção de contato cancelada!");
                    break;
                }

                break;
            }

        }

        private static void LerDados(int opcao)      // Lê os dados do contato e chama a função para adicionar o contato na lista
        {
            int Opcao = opcao;
            switch (Opcao)
            {
                case 1: //Adicionar Contato
                    {
                        Console.Write("Nome do Contato: ");
                        string nome = Console.ReadLine();

                        Console.Write("Telefone do Contato: ");
                        string telefone = Console.ReadLine();
                        while (!int.TryParse(telefone, out int verifica))
                        {
                            Console.WriteLine("\n> Telefone inválido, utilize apenas números!\n");
                            Console.Write("Telefone do Contato: ");
                            telefone = Console.ReadLine();
                        }

                        Console.Write("Email do Contato: ");
                        string email = Console.ReadLine();
                        while ((agenda.Find(x => x.Email == email)) != null)
                        {
                            Console.WriteLine("\n> Erro! Já existe um contato com esse E-mail cadastrado.");
                            Console.Write("Email do Contato: ");
                            email = Console.ReadLine();
                        }

                        while (!email.Contains("@") || !email.Contains("."))
                        {
                            Console.WriteLine("\n> E-mail inválido! Formato (exemplo@endereco.com.br)\n");
                            Console.Write("Email do Contato: ");
                            email = Console.ReadLine();
                        }

                        AdicionarContato(nome, telefone, email);
                        break;
                    }
                case 2: //Editar contato
                    {
                        Console.WriteLine("Informe o Nome, Telefone ou Email do contato que deseja editar:");
                        Console.Write("> ");
                        string infocontato = Console.ReadLine();

                        EditarContato(RetornaIndex(infocontato));

                        break;
                    }
                case 3: //Buscar contato
                    {
                        Console.WriteLine("Informe o Nome, Telefone ou Email do contato que deseja visualizar:");
                        Console.Write("> ");
                        string infocontato = Console.ReadLine();

                        BuscarContato(RetornaIndex(infocontato));

                        break;
                    }
                case 4: //Remover contato
                    {
                        Console.WriteLine("Informe o Nome, Telefone ou Email do contato que deseja remover:");
                        Console.Write("> ");
                        string infocontato = Console.ReadLine();

                        RemoverContato(RetornaIndex(infocontato));

                        break;
                    }
            }

        }

        //public static void CarregarAgenda()
        //{

        //    var length = new System.IO.FileInfo(caminho).Length;

        //    if (length > 0)
        //    {
        //        agenda.Clear();
        //        string[] arrayAgenda = File.ReadAllLines(caminho);
        //        for (int i = 0; i < arrayAgenda.Length; i++)
        //        {
        //            Contato contato = new Contato();

        //            string[] arrayAgendaSplit = arrayAgenda[i].Split(',');

        //            contato.Id = arrayAgendaSplit[0];
        //            contato.Nome = arrayAgendaSplit[1];
        //            contato.Telefone = arrayAgendaSplit[2];
        //            contato.Email = arrayAgendaSplit[3];

        //            agenda.Add(contato);
        //        }
        //    }

        //    Menu();
        //}

        public static void Menu()
        {

            var padding = "=".PadLeft(40, '=');
            int padLeft = 26;

            Console.WriteLine(padding);
            Console.WriteLine();
            Console.WriteLine("MENU AGENDA\n".PadLeft(padLeft));
            Console.WriteLine($"\n{DateTime.Now.ToString("dd/MM/yyyy - HH:mm".PadLeft(28))}\n");
            Console.WriteLine(padding);

            Console.WriteLine("\n[1] Adicionar Contato\n" +
                              "[2] Editar Contato\n" +
                              "[3] Buscar Contato\n" +
                              "[4] Remover Contato\n" +
                              "[5] Mostrar Agenda\n" +
                              "[0] Sair\n");
            Console.Write("> ");
            string opçãoMenu = Console.ReadLine();
            Console.WriteLine();

            switch (opçãoMenu)
            {
                case "1":   // Adicionar Contato
                    {
                        Console.WriteLine("---------- [Adicionar Contato] ----------\n");
                        LerDados(1);
                        Thread.Sleep(1500);
                        break;
                    }
                case "2":   // Editar Contato
                    {
                        Console.WriteLine("----------- [Editar Contato] ----------\n");
                        LerDados(2);
                        Thread.Sleep(1500);
                        break;
                    }
                case "3":   // Buscar Contato
                    {
                        Console.WriteLine("----------- [Buscar Contato] ----------\n");
                        LerDados(3);
                        Thread.Sleep(1500);
                        break;
                    }
                case "4":   // Remover Contato
                    {
                        Console.WriteLine("----------- [Remover Contato] ----------\n");
                        LerDados(4);
                        Thread.Sleep(1500);
                        break;
                    }
                case "5":   // Mostrar Agenda
                    {
                        Console.WriteLine("----------- [Mostrar Agenda] ----------\n");
                        MostrarAgenda();
                        Thread.Sleep(1500);
                        break;
                    }
                case "0":   // Sair
                    {
                        Environment.Exit(0);
                        break;
                    }

            }
            Menu();
        }


    }
}
