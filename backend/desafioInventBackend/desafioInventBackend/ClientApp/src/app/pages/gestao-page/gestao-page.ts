import { HttpResponse } from '@angular/common/http';
import { ChangeDetectorRef, Component, OnDestroy } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Subscription } from 'rxjs';
import Swal from 'sweetalert2';
import { IEquipamentoEletronico } from '../../../library/models/equipamento-eletronico.model';
import { EquipamentoEletronicoService } from '../../services/equipamento-eletronico-service';
import { DetalhesModal } from './detalhes-modal/detalhes-modal';
import { EquipamentoEletronicoFilter } from '../../../library/filters/equipamento-eletronico.filter';

@Component({
	selector: 'app-gestao-page',
	templateUrl: './gestao-page.html',
	styleUrl: './gestao-page.css',
	standalone: false,
})
export class GestaoPage implements OnDestroy {

	protected page = 1;
	protected itemsPerPage: number = 10;

	protected nome = '';
	protected tipoEquipamento = 0;
	protected dataInicio = '';
	protected dataFim = '';
	protected haEstoque = 1;

	listaTiposEquipamento = [
		{ id: 0, label: 'TODOS' },
		{ id: 1, label: 'PC' },
		{ id: 2, label: 'Notebook' },
		{ id: 3, label: 'Mouse' },
		{ id: 4, label: 'Teclado' },
		{ id: 5, label: 'Celular' },
	];

	estoque = [
		{ id: 1, label: 'TODOS' },
		{ id: 2, label: 'Há Estoque' },
		{ id: 3, label: 'Não Há Estoque' },
	];

	listaEquipamentosEletronicos: IEquipamentoEletronico[] = [];
	listaEquipamentosEletronicosFiltrada: IEquipamentoEletronico[] = [];
	listaEquipamentoEletronicoPaginada: IEquipamentoEletronico[] = [];

	private subscription: Subscription = new Subscription();

	constructor(
		private equipamentoEletronicoService: EquipamentoEletronicoService,
		private cdr: ChangeDetectorRef,
		private ngbModal: NgbModal
	) { }

	pesquisar() {

		const filter: EquipamentoEletronicoFilter = {
			nome: this.nome,
			equipamentoEmEstoque: this.haEstoque,
			dataInicio: this.dataInicio,
			dataFim: this.dataFim,
			tipoEquipamento: this.tipoEquipamento
		};

		this.subscription.add(
			this.equipamentoEletronicoService.pesquisarEquipamentoEletronico(filter).subscribe({
				next: (value: HttpResponse<IEquipamentoEletronico[]>) => {
					this.listaEquipamentosEletronicos = value.body!;
					this.listaEquipamentosEletronicosFiltrada = value.body!;
					this.updatePagination();
					this.cdr.detectChanges();
				}
			})
		);
	}

	excluir(equipamentoEletronico: IEquipamentoEletronico) {

		if (equipamentoEletronico.temEstoque) {
			Swal.fire({
				icon: 'warning',
				title: 'Atenção',
				html: 'Este equipamento não pode ser excluído,<br/>Pois ainda há produtos em estoque.',
				timer: 3000,
				showConfirmButton: false,
			});
			return;
		}

		this.confirmarExclusao().then((result) => {
			if (result.isConfirmed) {
				this.equipamentoEletronicoService.deletarEquipamentoEletronico(equipamentoEletronico.id).subscribe({
					next: () => {
						this.pesquisar();
					},
					complete: () => {
						Swal.fire({
							icon: 'success',
							title: 'Sucesso',
							text: 'equipamento excluído com sucesso.',
							timer: 3000,
							showConfirmButton: false,
						})
					}
				})
			}
		})
	}

	private confirmarExclusao() {
		return Swal.fire({
			icon: 'question',
			title: 'Atenção',
			text: 'Deseja realmente excluir este equipamento',
			showConfirmButton: true,
			confirmButtonText: 'Sim',
			confirmButtonColor: '#198754',
			showDenyButton: true,
			denyButtonText: 'Não',
			reverseButtons: true,
		});
	}

	visualizarDetalhes(equipamentoEletronico: IEquipamentoEletronico) {
		const modalRef = this.ngbModal.open(DetalhesModal, { backdrop: 'static', size: 'lg', centered: true });
		modalRef.componentInstance.equipamentoEletronico = equipamentoEletronico;
		modalRef.result.then((flagExcluir: boolean) => {
			if (flagExcluir) {
				this.excluir(equipamentoEletronico);
			}
		})
	}

	updatePagination() {
		const startIndex = (this.page - 1) * this.itemsPerPage;
		const endIndex = startIndex + this.itemsPerPage;
		this.listaEquipamentoEletronicoPaginada = this.listaEquipamentosEletronicosFiltrada.slice(startIndex, endIndex);
	}

	ngOnDestroy(): void {
		this.subscription.unsubscribe();
	}
}
