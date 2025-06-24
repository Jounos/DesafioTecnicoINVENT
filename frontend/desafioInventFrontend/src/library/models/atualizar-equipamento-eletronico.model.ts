import { EquipamentoEletronico, IEquipamentoEletronico } from "./equipamento-eletronico.model";

export interface IAtualizarEquipamentoEletronico extends IEquipamentoEletronico {
	id: number;
	dataInclusao?: string;
}

export class AtualizarEquipamentoEletronico extends EquipamentoEletronico implements IAtualizarEquipamentoEletronico {
	constructor(
		public id: number,
		public dataInclusao: string,
		public override nome: string,
		public override tipoEquipamento: number,
		public override quantidadeEstoque: number,
	) {
		super(nome, tipoEquipamento, quantidadeEstoque);
	}
}
