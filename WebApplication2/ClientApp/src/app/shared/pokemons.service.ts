import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, tap, catchError, throwError} from 'rxjs';
import { Pokemon, PokemonWithAttacks, Attack, newAttack } from './../shared/Interfaces';
@Injectable({
  providedIn: 'root'
})
export class PokemonsService {
  baseUrl = "https://localhost:7256/api"

  constructor(private http: HttpClient) {
  }

  getPokemon(pokemonId: string): Observable<PokemonWithAttacks> {
    return this.http.get<PokemonWithAttacks>(this.baseUrl + "/pokemons/" + pokemonId +"/?includeAttacks=true").pipe(
      tap(data => console.log('pokemon:', JSON.stringify(data))),
      catchError(this.handleError));
  }

  getPokemons(): Observable<Pokemon[]> {
    return this.http.get<Pokemon[]>(this.baseUrl+"/pokemons?pagination=false").pipe(
      tap(data => console.log('pokemons:', JSON.stringify(data))),
      catchError(this.handleError));
  }
  getAttacks(pokemonId: string): Observable<Attack[]>  {
    return this.http.get<Attack[]>(this.baseUrl + "/pokemons/" + pokemonId +"/attacks?pagination=false").pipe(
      tap(data => console.log('Attacks:', JSON.stringify(data))),
      catchError(this.handleError));
  }
  getAttack(pokemonId: string, attackId: string): Observable<Attack> {
    return this.http.get<Attack>(this.baseUrl + "/pokemons/" + pokemonId + "/attacks/" + attackId).pipe(
      tap(data => console.log('Attack:', JSON.stringify(data))),
      catchError(this.handleError));
  }

  postAttack(newAttack: newAttack, pokemonId: string): Observable<any> {
    console.log("Creando nuevo ataque");
    return this.http.post(this.baseUrl + "/pokemons/" + pokemonId + "/attacks", newAttack).pipe(
      tap(data => console.log('Ataque creado:', JSON.stringify(data))),
      catchError(this.handleError));
  }

  putAttack(newAttack: newAttack, pokemonId: string, attackId: string): Observable<any> {
    console.log("Editando ataque");
    return this.http.put(this.baseUrl + "/pokemons/" + pokemonId + "/attacks/" + attackId, newAttack).pipe(
      tap(_ => console.log('Ataque modificado con éxito.')),
      catchError(this.handleError));
  }

  deleteAttack(pokemonId: string, attackId: number): Observable<any> {
    console.log("Eliminando un ataque");
    return this.http.delete(this.baseUrl + "/pokemons/" + pokemonId + "/attacks/" + attackId).pipe(
      tap(_ => console.log('Ataque eliminado con éxito.')),
      catchError(this.handleError));
  }
  private handleError(err: HttpErrorResponse) {
    let errorMessage = err.error.message;
    console.error(errorMessage);
    return throwError(() => errorMessage)
  }

}
