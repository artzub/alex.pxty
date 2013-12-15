a([], L, L).
a([H|T], L,[H|R]):-
	a(T, L, R).

step(L, R):-
	a(S, [B, W|T], L),
	a(S, [W, B|T], R).

m(H, [H|_]).
m(H, [_|T]):-
	m(H, T).

f(H, [H|T]) :- !, pr([H|T]).
f(H, [R|T]) :-
	step(R, Cr),
	not(m(Cr, T)),
	f(H, [Cr, R|T]).

len(L, R):-
	len(L, 0, R).
len([], R, R).
len([_|T], C, L):-
	L1 is C + 1,
	len(T, L1, L).

pr(L):-
	len(L, R),
	pr(L, R).

pr([], _):-!.
pr([H|T], S) :-
	  S1 is S - 1
	, pr(T, S1)
	, write(S)
	, write(': ')
	, write(H)
	, nl
	.

run(S, F):-
	f(F, [S]).

:- write('Solution for Start state = [b, w, w, b, b, w, b, w] and Finish state = [w, w, w, w, b, b, b, b]'), nl,
	run([b, w, w, b, b, w, b, w], [w, w, w, w, b, b, b, b]).

