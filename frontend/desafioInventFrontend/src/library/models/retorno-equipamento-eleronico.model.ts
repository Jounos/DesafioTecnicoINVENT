import { EquipamentoEletronico, IEquipamentoEletronico } from "./equipamento-eletronico.model";

export interface IRetornoEquipamentoEletronico extends IEquipamentoEletronico {
	id: number;
	temEstoque: boolean;
	dataInclusao?: string;
	dataExclusao?: string;
}

export class RetornoEquipamentoEletronico extends EquipamentoEletronico implements IRetornoEquipamentoEletronico {
	constructor(
		public id: number,
		public temEstoque: boolean,
		public dataInclusao: string,
		public dataExclusao: string,
		public override nome: string,
		public override tipoEquipamento: number,
		public override quantidadeEstoque: number,
	) {
		super(nome, tipoEquipamento, quantidadeEstoque);
	}
}
