import { Component, inject, OnInit, signal, TemplateRef, WritableSignal } from '@angular/core';
import { ModalDismissReasons, NgbModal, NgbNavModule, NgbTooltip, NgbTooltipModule } from '@ng-bootstrap/ng-bootstrap';
import { CategoriaModel } from './models/categoria.model';
import { IconAvatar } from "../../shared/components/icon-avatar/icon-avatar";
import { StatusBadge } from "../../shared/components/status-badge/status-badge/status-badge";
import { FormControl, ReactiveFormsModule } from '@angular/forms';
import id from '@angular/common/locales/id';
import { CategoriaService } from './services/categoria.service';

@Component({
  selector: 'app-categorias',
  imports: [NgbNavModule, IconAvatar, StatusBadge, ReactiveFormsModule, NgbTooltipModule],
  templateUrl: './categorias.html',
  styleUrl: './categorias.css',
})
export class Categorias implements OnInit {

  private modalService = inject(NgbModal);
  private categoriaService = inject(CategoriaService);
  closeResult: WritableSignal<string> = signal('');

  nome = new FormControl('');
  descricao = new FormControl('');
  cor = new FormControl('');
  icone = new FormControl('');

  active = 1;
  editandoCategoria = false;
  idEditandoCategoria = '';

  listaCategorias = signal<CategoriaModel[]>([]);

  get listaReceitas(): CategoriaModel[] {
    return this.listaCategorias().filter((categoria) => categoria.tipo === 'receita');
  }

  get listaDespesas(): CategoriaModel[] {
    return this.listaCategorias().filter((categoria) => categoria.tipo === 'despesa');
  }

  ngOnInit(): void {
    this.carregarTodasCategorias();
  }

  carregarTodasCategorias() {
    this.categoriaService.obterTodasPorUsuario().subscribe({

      next: (dados) => {
        this.listaCategorias.set(dados);
      }
    })
  }

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

    const novaCategoria: CategoriaModel = {
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
      this.listaCategorias().push(novaCategoria);
    } else {
      novaCategoria.tipo = 'receita';
      this.listaCategorias().push(novaCategoria);
    }

    this.modalService.dismissAll();
  }

  excluirCategoria(id: string) {

  }

  editarCategoria() {
    const categoria = this.listaCategorias().find((c) => c.id === this.idEditandoCategoria);
    if (categoria) {
      categoria.nome = this.nome.value!;
      categoria.descricao = this.descricao.value!;
      categoria.cor = this.cor.value!;
      categoria.icone = this.icone.value!;
    }
    console.log(this.listaCategorias);
    this.modalService.dismissAll();
  }
}
