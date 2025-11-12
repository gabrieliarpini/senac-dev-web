import { Component, inject, signal, TemplateRef, WritableSignal } from '@angular/core';
import { ModalDismissReasons, NgbModal, NgbNavModule, NgbTooltip, NgbTooltipModule } from '@ng-bootstrap/ng-bootstrap';
import { CategoriaModel } from './models/categoria.model';
import { IconAvatar } from "../../shared/components/icon-avatar/icon-avatar";
import { StatusBadge } from "../../shared/components/status-badge/status-badge/status-badge";
import { FormControl, ReactiveFormsModule } from '@angular/forms';
import id from '@angular/common/locales/id';

@Component({
  selector: 'app-categorias',
  imports: [NgbNavModule, IconAvatar, StatusBadge, ReactiveFormsModule, NgbTooltipModule],
  templateUrl: './categorias.html',
  styleUrl: './categorias.css',
})
export class Categorias {
  private modalService = inject(NgbModal);
  closeResult: WritableSignal<string> = signal('');

  nome = new FormControl('');
  descricao = new FormControl('');
  cor = new FormControl('');
  icone = new FormControl('');

  active = 1;

  categorias: CategoriaModel[] = [
    {
      id: '1',
      nome: 'Salário',
      descricao: 'Recebimento mensal',
      cor: '#28a745',
      icone: 'ri-bank-line',
      tipo: 'receita',
      status: true
    },
    {
      id: '2',
      nome: 'Freelance',
      descricao: 'Trabalhos avulsos',
      cor: '#17a2b8',
      icone: 'ri-briefcase-line',
      tipo: 'receita',
      status: true
    },
    {
      id: '3',
      nome: 'Investimentos',
      descricao: 'Rendimentos de investimentos',
      cor: '#ffc107',
      icone: 'ri-line-chart-line',
      tipo: 'receita',
      status: true
    },

    /*despesa*/
    {
      id: '1',
      nome: 'Alimentação',
      descricao: 'Alimentação',
      cor: '#dc3545',
      icone: 'ri-restaurant-line',
      tipo: 'despesa',
      status: true
    },
    {
      id: '2',
      nome: 'Transporte',
      descricao: 'Despesas com transporte',
      cor: '#fd7e14',
      icone: 'ri-bus-line',
      tipo: 'despesa',
      status: true
    },
    {
      id: '3',
      nome: 'Lazer',
      descricao: 'Despesas com lazer',
      cor: '#ffc107',
      icone: 'ri-film-line',
      tipo: 'despesa',
      status: true
    },
  ];


  open(content: TemplateRef<any>) {
    this.modalService.open(content, { ariaLabelledBy: 'modal-basic-title' }).result.then(
      (result) => {
        this.closeResult.set(`Closed with: ${result}`);
      },
      (reason) => {
        this.closeResult.set(`Dismissed ${this.getDismissReason(reason)}`);
      },
    );
  }

  private getDismissReason(reason: any): string {
    switch (reason) {
      case ModalDismissReasons.ESC:
        return 'by pressing ESC';
      case ModalDismissReasons.BACKDROP_CLICK:
        return 'by clicking on a backdrop';
      default:
        return `with: ${reason}`;
    }
  }

  cadastrarCategoria() {
    console.log(this.nome.value);
    console.log(this.descricao.value);
    console.log(this.cor.value);
    console.log(this.icone.value);

    const novaCategoria : CategoriaModel = {
      id: '',
      nome: this.nome.value!,
      descricao: this.descricao.value!,
      cor: this.cor.value!,
      icone: this.icone.value!,
      tipo: '',
      status: true
    };

    if (this.active === 1) {
      novaCategoria.tipo = 'despesa';
      this.categorias.push(novaCategoria);
    } else {
      novaCategoria.tipo = 'receita';
      this.categorias.push(novaCategoria);
    }

    this.modalService.dismissAll();
  }

  deletarCategoriaDespesa(id: string) {
    this.categorias = this.categorias.filter(categoria => categoria.id !== id.toString());
  }

  deletarCategoriaReceita(id: string) {
    this.categorias = this.categorias.filter(categoria => categoria.id !== id.toString());
  }
}
