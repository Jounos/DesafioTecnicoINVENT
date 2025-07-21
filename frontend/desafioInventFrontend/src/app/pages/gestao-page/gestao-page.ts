import { HttpResponse } from '@angular/common/http';
import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import Swal from 'sweetalert2';
import { IRetornoEquipamentoEletronico } from '../../../library/models/retorno-equipamento-eleronico.model';
import { EquipamentoEletronicoService } from '../../services/equipamento-eletronico-service';
import dayjs from 'dayjs';

@Component({
	selector: 'app-gestao-page',
	templateUrl: './gestao-page.html',
	styleUrl: './gestao-page.css',
	standalone: false,
})
export class GestaoPage implements OnInit {

	protected page = 1;
	protected itemsPerPage: number = 10;
	sortColumn = '';
	sortDirection = 'asc';


	protected nome = "";
	protected tipoEquipamento = null;

	listaTiposEquipamento = [
		{ id: 1, label: 'PC' },
		{ id: 2, label: 'Notebook' },
		{ id: 3, label: 'Mouse' },
		{ id: 4, label: 'Teclado' },
	]

	listaEquipamentosEletronicos: IRetornoEquipamentoEletronico[] = [];
	listaEquipamentosEletronicosFiltrada: IRetornoEquipamentoEletronico[] = [];
	listaPaginada: IRetornoEquipamentoEletronico[] = [];

	constructor(
		private equipamentoEletronicoService: EquipamentoEletronicoService,
		private cdr: ChangeDetectorRef
	) { }

	ngOnInit(): void {
		this.listar();
		this.cdr.detectChanges();
	}

	listar() {
		this.equipamentoEletronicoService.listarEquipamentosEleronicos().subscribe({
			next: (value: HttpResponse<IRetornoEquipamentoEletronico[]>) => {
				if (value.body !== null) {
					this.listaEquipamentosEletronicos = value.body;
					this.listaEquipamentosEletronicosFiltrada = value.body.sort((a, b) => {
						if (dayjs(b.dataInclusao).isBefore(a.dataInclusao)) {
							return -1;
						} else {
							return 1;
						}
					});
					this.updatePagination();
					this.cdr.detectChanges();
				}
			}
		});
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

		this.listaEquipamentosEletronicosFiltrada = this.listaEquipamentosEletronicos.filter((ee) => {

			if ((this.nome !== '' || this.nome) && this.tipoEquipamento) {
				return (this.nome.length > 0 && ee.nome.toUpperCase().includes(this.nome.toUpperCase())) && ee.tipoEquipamento === this.tipoEquipamento;
			}

			if (!this.tipoEquipamento) {
				return this.nome.length > 0 && ee.nome.toUpperCase().includes(this.nome.toUpperCase());
			}

			return ee.tipoEquipamento === this.tipoEquipamento;
		});
		this.updatePagination();
		this.cdr.detectChanges();
	}

	filtrarAoLimparTipoEquipamento() {
		if (this.nome === '' || !this.nome) {
			this.listaEquipamentosEletronicosFiltrada = this.listaEquipamentosEletronicos;
		}

		this.listaEquipamentosEletronicosFiltrada = this.listaEquipamentosEletronicos.filter((ee) => ee.nome.toUpperCase().includes(this.nome.toUpperCase()));
		this.updatePagination();
		this.cdr.detectChanges();
	}

	excluir(ee: IRetornoEquipamentoEletronico) {

		if (ee.dataExclusao) {
			Swal.fire({
				icon: 'info',
				title: 'Atenção',
				text: 'Este equipamento eletrônico já foi excluído',
				showConfirmButton: false,
			});
			return;
		}

		if (ee.temEstoque) {
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
				this.equipamentoEletronicoService.deletarEquipamentoEletronico(ee.id).subscribe({
					next: (result: HttpResponse<boolean>) => {
						if (result.body) {
							Swal.fire({
								icon: 'success',
								title: 'equipamento excluído com sucesso.',
								timer: 3000,
								showConfirmButton: false,
							});
						}

						this.listar();
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

	updatePagination() {
		const startIndex = (this.page - 1) * this.itemsPerPage;
		const endIndex = startIndex + this.itemsPerPage;
		this.listaPaginada = this.listaEquipamentosEletronicosFiltrada.slice(startIndex, endIndex);
	}
}
