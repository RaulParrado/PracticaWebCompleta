import { Component, Inject, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { PokemonsService } from './../../shared/pokemons.service';
import { Pokemon, Attack, PokemonWithAttacks, newAttack } from './../../shared/Interfaces';
import { NgForm } from '@angular/forms';


@Component({
  selector: 'app-attacks',
  templateUrl: './editAttack.component.html',
//  styleUrls: ['./editAttack.component.css']
})

export class EditAttackComponent implements OnInit {
  constructor(private route: ActivatedRoute, private pokemonService: PokemonsService, private router: Router) { }

  newAttackTemplate: newAttack = {
    name:"",
    description: ""
  }

  newAttack: newAttack = { ...this.newAttackTemplate }
  pokemonId?: string | null;
  pokemon?: Pokemon;
  attackId?: string | null;

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => this.pokemonId = params.get('id'));
    if (this.pokemonId != null) {
      this.pokemonService.getPokemon(this.pokemonId).subscribe({
        next: pokemon => {
          this.pokemon = pokemon;
        }
      });
    }

    this.route.paramMap.subscribe(params => this.attackId = params.get('attackId'));
    if (this.pokemonId != null && this.attackId != null) {
      this.pokemonService.getAttack(this.pokemonId, this.attackId).subscribe({
        next: attack => {
          this.newAttack = attack;
        }
      });
    }
  }

  onSubmit(form: NgForm) {
    if (form.valid && this.pokemonId != null && this.attackId != null) {
      this.pokemonService.putAttack(this.newAttack, this.pokemonId, this.attackId).subscribe(
      )
    }
    else if (form.valid && this.pokemonId != null) {
      this.pokemonService.postAttack(this.newAttack, this.pokemonId).subscribe(
      )
    }
    this.router.navigate(['/pokemons/']);

  }

  onCancel() {
    this.router.navigate(['/pokemons/' + this.pokemonId]);

  }
}
