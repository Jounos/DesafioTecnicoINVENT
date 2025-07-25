import { HttpResponse } from '@angular/common/http';
import { ChangeDetectorRef, Component, OnDestroy, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Subscription } from 'rxjs';
import Swal from 'sweetalert2';
import { IEquipamentoEletronico } from '../../../library/models/equipamento-eletronico.model';
import { EquipamentoEletronicoService } from '../../services/equipamento-eletronico-service';
import { DetalhesModal } from './detalhes-modal/detalhes-modal';
import dayjs from 'dayjs';

@Component({
	selector: 'app-gestao-page',
	templateUrl: './gestao-page.html',
	styleUrl: './gestao-page.css',
	standalone: false,
})
export class GestaoPage implements OnInit, OnDestroy {

	protected page = 1;
	protected itemsPerPage: number = 10;

	protected nome = '';
	protected tipoEquipamento = 0;

	listaTiposEquipamento = [
		{ id: 1, label: 'PC' },
		{ id: 2, label: 'Notebook' },
		{ id: 3, label: 'Mouse' },
		{ id: 4, label: 'Teclado' },
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

	ngOnInit(): void {
		this.listar();
	}

	listar() {
		this.subscription.add(
			this.equipamentoEletronicoService.listarTodosEquipamentosEleronicos().subscribe({
				next: (value: HttpResponse<IEquipamentoEletronico[]>) => {
					if (value.body?.length === 0) {
						Swal.fire({
							icon: 'info',
							title: 'Atenção',
							text: 'Nenhum Equipamento Eletrônico foi encontrado',
							showConfirmButton: false,
						});
						return;
					}

					this.listaEquipamentosEletronicos = value.body!;
					this.listaEquipamentosEletronicosFiltrada = value.body!.sort((a, b) => {
						if (dayjs(b.dataInclusao).isBefore(a.dataInclusao)) {
							return -1;
						} else {
							return 1;
						}
					});
					this.updatePagination();
					this.cdr.detectChanges();
				}
			})
		);
	}

	filtrar() {
		if (!this.listaEquipamentosEletronicos || this.listaEquipamentosEletronicos?.length === 0) {
			return
		}

		if ((this.nome === '' || !this.nome) && !this.tipoEquipamento) {
			this.listaEquipamentosEletronicosFiltrada = this.listaEquipamentosEletronicos;
			this.updatePagination();
			return;
		}

		this.listaEquipamentosEletronicosFiltrada = this.listaEquipamentosEletronicos.filter((equipamentoEletronico) => {

			if ((this.nome !== '' || this.nome) && this.tipoEquipamento) {
				return (this.nome.length > 0 && equipamentoEletronico.nome.toUpperCase().includes(this.nome.toUpperCase())) && equipamentoEletronico.tipoEquipamento === this.tipoEquipamento;
			}

			if (!this.tipoEquipamento) {
				return this.nome.length > 0 && equipamentoEletronico.nome.toUpperCase().includes(this.nome.toUpperCase());
			}

			return equipamentoEletronico.tipoEquipamento === this.tipoEquipamento;
		});
		this.updatePagination();
		this.cdr.detectChanges();
	}

	filtrarAoLimparTipoEquipamento() {
		if (this.nome === '' || !this.nome) {
			this.listaEquipamentosEletronicosFiltrada = this.listaEquipamentosEletronicos;
		}

		this.listaEquipamentosEletronicosFiltrada = this.listaEquipamentosEletronicos.filter((equipamentoEletronico) => equipamentoEletronico.nome.toUpperCase().includes(this.nome.toUpperCase()));
		this.updatePagination();
		this.cdr.detectChanges();
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
						Swal.fire({
							icon: 'success',
							title: 'Sucesso',
							text: 'equipamento excluído com sucesso.',
							timer: 3000,
							showConfirmButton: false,
						}).then(() => {
							this.listar();
						});
					},
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
