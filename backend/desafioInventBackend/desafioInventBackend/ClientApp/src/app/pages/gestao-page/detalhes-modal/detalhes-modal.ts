import { Component, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { IEquipamentoEletronico } from '../../../../library/models/equipamento-eletronico.model';

@Component({
	selector: 'app-detalhes-modal',
	templateUrl: './detalhes-modal.html',
	styleUrl: './detalhes-modal.css',
	standalone: false,
})
export class DetalhesModal {

	@Input({ required: true }) equipamentoEletronico!: IEquipamentoEletronico;

	constructor(private ngbActiveModal: NgbActiveModal) { }

	fechar(flagExcluir: boolean = false) {
		this.ngbActiveModal.close(flagExcluir);
	}
}
