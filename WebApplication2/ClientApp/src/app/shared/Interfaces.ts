export interface Attack {
  id: number;
  name: string;
  description: string;
}
export interface newAttack {
  name: string;
  description: string;
}
export interface Pokemon {
  id: number;
  name: string;
  description: string;
}
export interface PokemonWithAttacks {
  id: number;
  name: string;
  description: string;
  attacks: Attack[];
}
