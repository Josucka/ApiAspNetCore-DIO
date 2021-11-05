using System;

namespace ApiAspNetCore.Exception
{
    public class JogoJaCadastradoException : Exception
    {
        public JogoJaCadastradoException() : base("Este jogo ja esta cadastrado") { }
    }
}
