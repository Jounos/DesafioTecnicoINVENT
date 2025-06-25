import { HttpResponse } from '@angular/common/http';
import { ChangeDetectorRef, Component, computed, model, OnInit, signal } from '@angular/core';
import { IRetornoEquipamentoEletronico } from '../../../library/models/retorno-equipamento-eleronico.model';
import { EquipamentoEletronicoService } from '../../../services/equipamento-eletronico-service';
import Swal from 'sweetalert2';

@Component({
	selector: 'app-gestao-page',
	templateUrl: './gestao-page.html',
	styleUrl: './gestao-page.css',
	standalone: false,
})
export class GestaoPage implements OnInit {

	page = 4;

	protected nome = "";
	protected tipoEquipamento = null;

	listaTiposEquipamento = [
		{ id: 1, label: 'PC'},
		{ id: 2, label: 'Notebook'},
		{ id: 3, label: 'Mouse'},
		{ id: 4, label: 'Teclado'},
	]

	listaEquipamentosEletronicos: IRetornoEquipamentoEletronico[] = [];
	listaEquipamentosEletronicosFiltrada: IRetornoEquipamentoEletronico[] = [];

	constructor(
		private equipamentoEletronicoService: EquipamentoEletronicoService,
		private cdr: ChangeDetectorRef
	) {	}

	ngOnInit(): void {
		this.listar();
		this.cdr.detectChanges();
	}

	listar() {
		this.equipamentoEletronicoService.listarEquipamentosEleronicos().subscribe({
			next: (value: HttpResponse<IRetornoEquipamentoEletronico[]>) => {
				if (value.body !== null) {
					this.listaEquipamentosEletronicos = value.body;
					this.listaEquipamentosEletronicosFiltrada = value.body;
					return;
				}

			}
		});
	}

	filtrar() {
		this.listaEquipamentosEletronicosFiltrada = this.listaEquipamentosEletronicos.filter((ee) => ee.nome.toUpperCase().includes(this.nome.toUpperCase()) || ee.tipoEquipamento == this.tipoEquipamento);
	}

	excluir(ee: IRetornoEquipamentoEletronico) {

		console.table(ee);
		if (ee.temEstoque) {
			Swal.fire({
				icon: 'warning',
				title: 'Atenção',
				text: 'Este equipamento não pode ser excluído,\n pois ainda há em estoque.',
				timer: 3000,
				showConfirmButton: false,
			});
			return;
		}

		this.confirmarExclusao().then((result) => {
			if (result.isConfirmed) {
				this.equipamentoEletronicoService.deletarEquipamentoEletronico(ee.id).subscribe({
					next: (result: HttpResponse<void>) => {
						Swal.fire({
							icon: 'success',
							title: 'equipamento excluido com sucesso.',
							timer: 3000
						});

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

}
