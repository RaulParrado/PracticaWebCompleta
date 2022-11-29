import { Component, Inject, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { PokemonsService } from './../shared/pokemons.service';
import { Pokemon, Attack, PokemonWithAttacks, newAttack } from './../shared/Interfaces';

@Component({
  selector: 'app-attacks',
  templateUrl: './attacks.component.html'
})

export class AttackComponent implements OnInit {
  constructor(private route: ActivatedRoute, private router: Router, private pokemonsService: PokemonsService) {

  }
  pokemonId?: string | null;
  attacks: Attack[] = [];
  pokemon?: PokemonWithAttacks;
  successMessage?: string;
  errorMessage?: string;


  

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => this.pokemonId = params.get('id'));
    this.loadPokemon();
 
  }

  loadPokemon(): void {
    if (this.pokemonId != null) {
      this.pokemonsService.getPokemon(this.pokemonId).subscribe({
        next: pokemon => {
          this.attacks = pokemon.attacks;
          this.pokemon = pokemon;
        }
      });
    }
  }

  newAttack(): void {
    if (this.pokemon != null) {
      this.router.navigate(['/pokemons/' + this.pokemon.id + "/new"])
    }
  }
  editAttack(attack: Attack) {
    if (this.pokemonId != null) {
      this.router.navigate(['/pokemons/' + this.pokemonId + '/edit/' + attack.id]);
    }
  }
  deleteAttack(attack: Attack) {
    if (this.pokemonId != null) {
        this.pokemonsService.deleteAttack(this.pokemonId, attack.id).subscribe(
      );
      this.router.navigate(['/pokemons/']);
      }
  }
}
