﻿using SigecomTestesUI.Config;
using SigecomTestesUI.Services;
using SigecomTestesUI.Sigecom.Cadastros.Categoria.Model;
using System;
using System.Collections.Generic;
using Autofac;
using NUnit.Framework;
using SigecomTestesUI.ControleDeInjecao;
using SigecomTestesUI.Sigecom.Cadastros.Categoria.PesquisaDeCategoria;

namespace SigecomTestesUI.Sigecom.Cadastros.Categoria
{
    public class CadastroDeCategoriaPage : PageObjectModel
    {
        private readonly Dictionary<string, string> _dadosDeCategoria;
        public CadastroDeCategoriaPage(DriverService driver, Dictionary<string, string> dadosDeCategoria) : base(driver) => 
            _dadosDeCategoria = dadosDeCategoria;

        public bool ClicarNaOpcaoDoMenu() =>
            AcessarOpcaoMenu(CadastroDeCategoriaModel.BotaoMenuCadastro);

        public bool ClicarNaOpcaoDoSubMenu() =>
            AcessarOpcaoSubMenu(CadastroDeCategoriaModel.BotaoSubMenuCategoria);

        private void ClicarNoBotaoNovoCategoria()
        {
            try
            {
                DriverService.ClicarBotaoName(CadastroDeCategoriaModel.BotaoNovoCategoria);
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private void ClicarNoBotaoNovo()
        {
            try
            {
                DriverService.ClicarBotaoName(CadastroDeCategoriaModel.BotaoNovo);
            }
            catch (Exception)
            {
                // ignored
            }
        }

        public bool PreencherCamposDaCategoriaGrade()
        {
            try
            {
                PreencherCamposBaseDaCategoria();
                DriverService.SelecionarDoisItensDaGrid(CadastroDeCategoriaModel.ElementoGrid, 1);
                DriverService.SelecionarDoisItensDaGrid(CadastroDeCategoriaModel.ElementoGrid, 2);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void PreencherCamposDaCategoria(string toggleDeCategoria)
        {
            try
            {
                PreencherCamposBaseDaCategoria();
                DriverService.ClicarNoToggleSwitchPeloId(toggleDeCategoria);
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private void PreencherCamposBaseDaCategoria()
        {
            DriverService.DigitarNoCampoId(CadastroDeCategoriaModel.ElementoNomeGrupo, _dadosDeCategoria["Grupo"]);
            DriverService.DigitarNoCampoId(CadastroDeCategoriaModel.ElementoMarkup, _dadosDeCategoria["Markup"]);
            DriverService.SelecionarItemComboBox(CadastroDeCategoriaModel.ElementoTipoGrupo, 1);
        }

        public bool Gravar()
        {
            try
            {
                DriverService.ClicarBotaoName(CadastroDeCategoriaModel.BotaoGravar);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void FecharJanelaCadastroDeCategoriaComEsc(string telaAtual)
        {
            try
            {
                DriverService.FecharJanelaComEsc(telaAtual);
            }
            catch (Exception)
            {
                // ignored
            }
        }

        public void RealizarFluxoDeCadastroDeCategoria(string toggle)
        {
            // Arange
            AbrirTelaDeCategoriaParaTeste();

            // Act
            PreencherCamposDaCategoria(toggle);
            Gravar();

            // Assert
            PesquisarCategoriaGravada();
        }

        public void AbrirTelaDeCategoriaParaTeste()
        {
            ClicarNaOpcaoDoMenu();
            ClicarNaOpcaoDoSubMenu();
            ClicarNoBotaoNovoCategoria();
            ClicarNoBotaoNovo();
        }

        public void PesquisarCategoriaGravada()
        {
            FecharJanelaCadastroDeCategoriaComEsc(CadastroDeCategoriaModel.ElementoTelaCadastroDeCategoria);
            var pesquisaDeCategoriaPage = RetornarPesquisaDeCategoriaPage();
            pesquisaDeCategoriaPage.PesquisarCategoriaNaTelaDeControle(_dadosDeCategoria["Grupo"]);
            Assert.True(pesquisaDeCategoriaPage.VerificarSeExisteCategoriaNaGrid(_dadosDeCategoria["Grupo"]));
            FecharJanelaCadastroDeCategoriaComEsc(CadastroDeCategoriaModel.ElementoTelaControleDeCategoria);
        }

        private PesquisaDeCategoriaPage RetornarPesquisaDeCategoriaPage()
        {
            using var beginLifetimeScope = ControleDeInjecaoAutofac.Container.BeginLifetimeScope();
            var resolvePesquisaDeCategoriaPage = beginLifetimeScope.Resolve<Func<DriverService, PesquisaDeCategoriaPage>>();
            return resolvePesquisaDeCategoriaPage(DriverService);
        }
    }
}
