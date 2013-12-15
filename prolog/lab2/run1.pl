/*l(en).
l(fr).
l(de).
l(it).

d(s).
d(b).
d(h).
d(m).

ndl(s, en).
ndl(h, de).
ndl(b, L):-L \== de.

dl(D, L):-
	d(D), l(L),
%	not(ndl(D, L)),

ndl(_, L, L).
ndl(_, fr, de).
ndl(_, de, fr).

dl(D, L, L1):-
	(spl(D, _, L); spl(_, D, L)),
	(spl(D, _, L1); spl(_, D, L1)),
	not(ndl(D, L, L1)).



sp(s, b).
sp(b, s).
sp(s, m).
sp(m, s).
sp(b, h).
sp(h, b).

sp(s, h, b).
sp(s, b, h).
sp(h, s, b).
sp(h, b, s).
sp(b, s, h).
sp(b, h, s).

nol(D, D, _).
ol(D, D1, L):-
	dl(D, L), dl(D1, L),
	not(nol(D, D1, L)).

spl(D, D1, L):-
	sp(D, D1),
	ol(D, D1, L).

ol(D, D1, D2, L):-
	ol(D, D1, L),
	ol(D1, D2, L),
	ol(D, D2, L),
	sp(D, D1, D2).*/

main:- set2(b), set2(c), set2(m), set2(p),
         lan3(_, _, _), not(lan3(c, m, p)),
         nl, nl, lan(X,Y,Z), write(X), write(' '), write(Y), write(' '), write(Z), nl, fail.
main.
%
set2(A) :- l(B), l(C), C \== B, ass(A,B,C).
%
ass(_, fr, de):- !, fail.
ass(p, en, _):- !, fail.
ass(p, X, Y) :- lan(b, Z, Q), X \== Z, X \== Q, Y \== Z, Y \== Q, !, fail.
ass(p, X, Y) :- lan(m, Z, Q), X \== Z, X \== Q, Y \== Z, Y \== Q, !, fail.
ass(b, X, Y) :- X \== de, Y \== de, !, fail.
ass(c, de, _) :- !, fail.
ass(c, _, de) :- !, fail.
ass(c, X, Y) :- lan(b, Z, Q), X \== Z, X \== Q, Y \== Z, Y \== Q, !, fail.
ass(m, X, _) :- oneL(b, X), !, fail.
ass(m, _, X) :- oneL(b, X), !, fail.
ass(A, B, C) :- assert(lan(A, B, C)).
ass(A, _, _) :- retract(lan(A, _, _)), fail.
%
lan3(A,B,C):- oneL(A, D), oneL(B, D), B \== A, oneL(C, D), C \== B.
%
oneL(A,B):- lan(A, B, _); lan(A, _, B).
%
l(en). l(fr). l(de). l(it).
