namespace AgendaBeca
{
    class Contato
    {
        public string Nome { get; set; }

        public string Telefone { get; set; }

        public string Email { get; set; }

        public string Id { get; set; }
        public Contato()
        {

        }
        public Contato(string nome, string telefone, string email)
        {
            Nome = nome.ToUpper();
            Telefone = telefone;
            Email = email;
        }

        //public override string ToString()
        //{
        //    return $"#{Id + 1} - Nome: {Nome} | Telefone: {Telefone} | E-mail: {Email}";
        //}
    }
}
