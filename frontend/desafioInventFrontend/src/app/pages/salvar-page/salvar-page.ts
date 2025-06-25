import { HttpResponse } from '@angular/common/http';
import { ChangeDetectorRef, Component, inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import Swal from 'sweetalert2';
import { AtualizarEquipamentoEletronico } from '../../../library/models/atualizar-equipamento-eletronico.model';
import { EquipamentoEletronico } from '../../../library/models/equipamento-eletronico.model';
import { IRetornoEquipamentoEletronico } from '../../../library/models/retorno-equipamento-eleronico.model';
import { EquipamentoEletronicoService } from '../../../services/equipamento-eletronico-service';

@Component({
	selector: 'app-salvar-page',
	templateUrl: './salvar-page.html',
	styleUrl: './salvar-page.css',
	standalone: false,
})
export class SalvarPage implements OnInit {

	protected equipamentoEletronicoId: number | undefined;
	private eeDataInclusao: string = '';

	private formBuilder = inject(FormBuilder);

	protected form: FormGroup = this.formBuilder.group({
		nome: [null, Validators.required],
		tipoEquipamento: [null, Validators.required],
		quantidadeEstoque: [null, [Validators.required, Validators.min(0)]]
	});;

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
		this.equipamentoEletronicoId = this.route.snapshot.paramMap.get('id') ? Number(this.route.snapshot.paramMap.get('id')) : undefined;
		if (this.equipamentoEletronicoId) {
			this.buscarEquipamentoEletronico(this.equipamentoEletronicoId);
		}
		this.cdr.detectChanges();
	}

	private buscarEquipamentoEletronico(id: number) {
		this.equipamentoEletronicoService.buscarEquipamentoEletronicoPorId(id).subscribe({
			next: (value: HttpResponse<IRetornoEquipamentoEletronico>) => {
				this.eeDataInclusao = (value.body as IRetornoEquipamentoEletronico)?.dataInclusao!;
				this.preencherFormulario((value.body as IRetornoEquipamentoEletronico));
			},
		});
	}

	preencherFormulario(ee: IRetornoEquipamentoEletronico) {
		this.form.patchValue({
			nome: ee.nome,
			tipoEquipamento: ee.tipoEquipamento,
			quantidadeEstoque: ee.quantidadeEstoque
		})
	}

	public salvar() {

		if (!this.equipamentoEletronicoId) {
			this.cadastrar();
			return;
		}

		this.atualizar();
	}

	private cadastrar() {
		var equipamentoEletronico: EquipamentoEletronico = {
			nome: this.form.value.nome,
			tipoEquipamento: this.form.value.tipoEquipamento,
			quantidadeEstoque: this.form.value.quantidadeEstoque,
		};

		this.equipamentoEletronicoService.cadastrarEquipamentoEletronico(equipamentoEletronico).subscribe({
			next: (value: HttpResponse<IRetornoEquipamentoEletronico>) => {
				if (value.status !== 201 ){
					return;
				}

				Swal.fire({
					icon: 'success',
					title: 'Sucesso',
					text: 'Equipamento eletrônico cadastrado com sucesso.',
				});
			},
			error: () => {

				Swal.fire({
					icon: 'error',
					title: 'Ocorreu um error',
					text: 'Entre em contato com a equipe de desenvolviemnto.',
					timer: 3000,
				});
			},
		});
	}

	private atualizar(){
		var equipamentoEletronico: AtualizarEquipamentoEletronico = {
			id: this.equipamentoEletronicoId!,
			nome: this.form.value.nome,
			tipoEquipamento: this.form.value.tipoEquipamento,
			quantidadeEstoque: this.form.value.quantidadeEstoque,
			dataInclusao: this.eeDataInclusao,

		};

		this.equipamentoEletronicoService.atualiarEquipamentoEletronico(this.equipamentoEletronicoId!, equipamentoEletronico).subscribe({
			next: (value: HttpResponse<IRetornoEquipamentoEletronico>) => {
				if (value.status !== 201 ){
					return;
				}

				Swal.fire({
					icon: 'success',
					title: 'Sucesso',
					text: 'Equipamento eletrônico editado com sucesso.',
				});
			},
			error: () => {

				Swal.fire({
					icon: 'error',
					title: 'Ocorreu um error',
					text: 'Entre em contato com a equipe de desenvolviemnto.',
					timer: 3000,
				});
			},
		})
	}
}
