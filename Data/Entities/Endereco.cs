﻿namespace WS.WorkerExample.Data.Entities
{
    public class Endereco
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Logradouro { get; set; } = string.Empty;
        public string Numero { get; set; } = string.Empty;
        public string Bairro { get; set; } = string.Empty;
        public string Cidade { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public string CEP { get; set; } = string.Empty;
    }
}