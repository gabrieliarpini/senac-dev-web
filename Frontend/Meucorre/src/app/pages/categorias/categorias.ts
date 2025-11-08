import { Component } from '@angular/core';
import { NgbNavModule } from '@ng-bootstrap/ng-bootstrap';
import { CategoriaModel } from './models/categoria.model';
import { IconAvatar } from "../../shared/components/icon-avatar/icon-avatar";
import { StatusBadge } from "../../shared/components/status-badge/status-badge/status-badge";

@Component({
  selector: 'app-categorias',
  imports: [NgbNavModule, IconAvatar, StatusBadge],
  templateUrl: './categorias.html',
  styleUrl: './categorias.css',
})
export class Categorias {
  active = 1;

  categorias_receitas: CategoriaModel[] = [
    {
      id: '1', 
      nome: 'Salário', 
      descricao: 'Recebimento mensal', 
      cor: '#28a745', 
      icone: 'ri-bank-line', 
      status: true
    },
    {
      id: '2',
      nome: 'Freelance',
      descricao: 'Trabalhos avulsos',
      cor: '#17a2b8',
      icone: 'ri-briefcase-line',
      status: true
    },
    {
      id: '3',
      nome: 'Investimentos',
      descricao: 'Rendimentos de investimentos',
      cor: '#ffc107',
      icone: 'ri-line-chart-line',
      status: true
    },
  ];

  categorias_despesas: CategoriaModel[] = [
    {
      id: '1',
      nome: 'Alimentação',
      descricao: 'Alimentação',
      cor: '#dc3545',
      icone: 'ri-restaurant-line',
      status: true
    },
    {
      id: '2',
      nome: 'Transporte',
      descricao: 'Despesas com transporte',
      cor: '#fd7e14',
      icone: 'ri-bus-line',
      status: true
    },
    {
      id: '3',
      nome: 'Lazer',
      descricao: 'Despesas com lazer',
      cor: '#ffc107',
      icone: 'ri-film-line' ,
      status: true
    },
  ];
}