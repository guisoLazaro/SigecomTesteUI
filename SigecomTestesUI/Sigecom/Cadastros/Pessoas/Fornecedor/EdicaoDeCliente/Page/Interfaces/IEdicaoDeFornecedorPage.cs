﻿using SigecomTestesUI.Sigecom.Cadastros.Pessoas.PesquisaPessoa;

namespace SigecomTestesUI.Sigecom.Cadastros.Pessoas.Fornecedor.EdicaoDeCliente.Page.Interfaces
{
    public interface IEdicaoDeFornecedorPage
    {
        void PesquisarFornecedorQueSeraEditado(EdicaoDeFornecedorBasePage edicaoDeColaboradorBasePage);
        void VerificarDadosDaPessoa();
        void PreencherAsInformacoesDaPessoasNaEdicao();
        void VerificarDadosDaPessoaEditados();
        void FluxoDePesquisaDaPessoaEditado(EdicaoDeFornecedorBasePage edicaoDeColaboradorBasePage,
            PesquisaDePessoaPage pesquisaDePessoaPage);
    }
}
