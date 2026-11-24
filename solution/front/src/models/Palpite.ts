export enum Resultado {
  Vitoria = 1,
  Derrota = 2,
  Empate = 3,
}

export interface Aposta {
  idAposta?: number;       
  idJogo: number;
  resultadoSelecao1: Resultado;
  resultadoSelecao2?: Resultado; 
  odd: number;        
  oldAposta?: number;      
  criadoEm?: string;       
}