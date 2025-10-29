export interface IEquipamentoEletronicoFilter {
	nome?: string;
	tipoEquipamento?: number;
	dataInicio?: string;
	dataFim?: string;
	equipamentoEmEstoque?: number;
}

export class EquipamentoEletronicoFilter implements IEquipamentoEletronicoFilter {
	constructor(
		public nome?: string,
		public tipoEquipamento?: number,
		public dataInicio?: string,
		public dataFim?: string,
		public equipamentoEmEstoque?: number
	) { }


}
