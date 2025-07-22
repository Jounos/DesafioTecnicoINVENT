import { HttpResponse } from '@angular/common/http';
import { ChangeDetectorRef, Component, inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import Swal from 'sweetalert2';
import { EquipamentoEletronicoService } from '../../services/equipamento-eletronico-service';
import { IEquipamentoEletronico } from '../../../library/models/equipamento-eletronico.model';

@Component({
	selector: 'app-salvar-page',
	templateUrl: './salvar-page.html',
	styleUrl: './salvar-page.css',
	standalone: false,
})
export class SalvarPage implements OnInit {

	protected equipamentoEletronicoId: string | undefined;

	private formBuilder = inject(FormBuilder);

	protected form: FormGroup = this.formBuilder.group({
		nome: [null, [Validators.required, Validators.minLength(3), Validators.maxLength(100)]],
		tipoEquipamento: [null, [Validators.required, Validators.min(1), Validators.max(4)]],
		quantidadeEstoque: [null, [Validators.required, Validators.min(0)]]
	});

	protected tipoEquipamento: number = 0;
	protected listaTiposEquipamento = [
		{ id: 1, label: 'PC' },
		{ id: 2, label: 'Notebook' },
		{ id: 3, label: 'Mouse' },
		{ id: 4, label: 'Teclado' },
	];

	constructor(
		private equipamentoEletronicoService: EquipamentoEletronicoService,
		private cdr: ChangeDetectorRef,
		private route: ActivatedRoute,
		private router: Router
	) { }

	ngOnInit(): void {
		this.equipamentoEletronicoId = this.route.snapshot.paramMap.get('id') ? String(this.route.snapshot.paramMap.get('id')) : undefined;
		if (this.equipamentoEletronicoId) {
			this.buscarEquipamentoEletronico(this.equipamentoEletronicoId);
		}
		this.cdr.detectChanges();
	}

	private buscarEquipamentoEletronico(id: string) {
		this.equipamentoEletronicoService.buscarEquipamentoEletronicoPorId(id).subscribe({
			next: (value: HttpResponse<IEquipamentoEletronico>) => {
				if (!value.body) {
					Swal.fire({
						icon: 'error',
						title: 'Error',
						text: "Atenção: error ao buscar o equipamento eletrônico<br/>Entre em contato com a equipe de desenvolvimento!",
						showConfirmButton: false,
					}).then(() => {
						return;
					})
				}
				this.preencherFormulario(value.body as IEquipamentoEletronico);
			},
		});
	}

	preencherFormulario(ee: IEquipamentoEletronico) {
		this.form.patchValue({
			nome: ee.nome,
			tipoEquipamento: ee.tipoEquipamento,
			quantidadeEstoque: ee.quantidadeEstoque
		})
	}

	public salvar() {

		if (this.form.invalid) {

			Swal.fire({
				icon: 'info',
				title: 'Atenção',
				text: 'Informe todos os campos.',
				showConfirmButton: false
			});
			return;
		}

		if (!this.equipamentoEletronicoId) {
			this.cadastrar();
			return;
		}

		this.atualizar();
	}

	private cadastrar() {
		this.equipamentoEletronicoService.cadastrarEquipamentoEletronico(this.getEquipamentoEletronico()).subscribe({
			next: () => {
				Swal.fire({
					icon: 'success',
					title: 'Sucesso',
					text: 'Equipamento eletrônico cadastrado com sucesso.',
					showConfirmButton: false,
					timer: 3000
				}).then(() => {
					this.router.navigate(['/']);
				});
			},
		});
	}

	private atualizar(){

		const equipamentoEletronico = {
			id: this.equipamentoEletronicoId!,
			...this.getEquipamentoEletronico(),
		}

		this.equipamentoEletronicoService.atualizarEquipamentoEletronico(this.equipamentoEletronicoId!,  equipamentoEletronico).subscribe({
			next: () => {
				Swal.fire({
					icon: 'success',
					title: 'Sucesso',
					text: 'Equipamento eletrônico editado com sucesso.',
					showConfirmButton: false,
					timer: 3000
				});
			},
		})
	}

	private getEquipamentoEletronico() {
		return {
			nome: this.form.value.nome,
			tipoEquipamento: this.form.value.tipoEquipamento,
			quantidadeEstoque: this.form.value.quantidadeEstoque,
		};
	}
}
