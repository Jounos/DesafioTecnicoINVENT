export interface IEquipamentoEletronico {
	id: string;
	nome: string;
	tipoEquipamento: number;
	quantidadeEstoque: number;
	temEstoque: boolean;
	dataInclusao?: string;
}

export class EquipamentoEletronico implements IEquipamentoEletronico {

	constructor(
		public id: string,
		public nome: string,
		public tipoEquipamento: number,
		public quantidadeEstoque: number,
		public temEstoque: boolean,
		public dataInclusao: string,
	) { }
}
