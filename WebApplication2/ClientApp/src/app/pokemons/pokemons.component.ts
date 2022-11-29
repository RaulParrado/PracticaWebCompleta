import { Component, Inject, OnDestroy, OnInit } from '@angular/core';
import { PokemonsService } from './../shared/pokemons.service';
import { Subscription } from 'rxjs';
import { Pokemon } from './../shared/Interfaces';

@Component({
  selector: 'app-pokemons',
  templateUrl: './pokemons.component.html'
})

export class PokemonsComponent implements OnInit, OnDestroy {
  sub!: Subscription;
  pokemons: Pokemon[] = [];

  constructor(private pokemonsService: PokemonsService) {

  }

  ngOnDestroy(): void {
    this.sub.unsubscribe();
  }
  ngOnInit(): void {
    this.sub = this.pokemonsService.getPokemons().subscribe({
      next: pokemons => {
        this.pokemons = pokemons;

      }
    })
  }
}
