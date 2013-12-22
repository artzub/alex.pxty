list_front([], _, [], []) :-!.

list_front([H|L], 1, [H], L) :-!.

list_front([H|T], N, [H|F], R) :-
	N1 is N - 1,
	list_front(T, N1, F, R).
	
string_front(Str, L, F, E) :-
	string_chars(Str, Chs),
	list_front(Chs, L, Fs, Es),
	string_chars(F, Fs),
	string_chars(E, Es).