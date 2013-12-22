%% list_front(source, len, first, end); разделяет входящий список на два начиная с начала
list_front([], _, [], []) :-!.

list_front([H|L], 1, [H], L) :-!.

list_front([H|T], N, [H|F], R) :-
	N1 is N - 1
	, list_front(T, N1, F, R)
	.

%% выдает в F строку из Str длиной L в E остаток от строки Str
string_front(Str, L, F, E) :-
	string_chars(Str, Chs),
	list_front(Chs, L, Fs, Es),
	string_chars(F, Fs),
	string_chars(E, Es).

rev([], _, []).
rev([H|L],  [])



core("уч").

prefix("вы").
prefix("из").
prefix("об").

sufix("и").
sufix("л").
sufix("ак").

end("а", gen("женский"), num(единственный)).
end("о", gen("средний"), num("ед")).
end("и", n, num("мн")).

find(W, M4):-
	extPrefix(W, [], W1, M1)
	, extCore(W1, M1, W2, M2)
	, extSufix(W2, M2, W3, M3)
	, extEnd(W3, M3, E, M4)
	, E = ""
	,
	, !
	.

extPrefix(W, M, W, M).
extPrefix(W, M, E, [pref(P)|M]):-
	prefix(P)
	, string_length(P, L)
	, string_length(W, L1)
	, L =< L1
	, string_front(W, L, P1, E)
	, P1 = P
	.

extCore(W, M, E, [cor(C)|M]):-
	core(C)
	, string_length(C, L)
	, string_length(W, L1)
	, L =< L1
	, string_front(W, L, C1, E)
	, C1 = C
	.

listSufix(W, W, Ls, suf(Ls)).
listSufix(W, E, Ls, C) :-
	sufix(S)
	, string_length(S, L)
	, string_length(W, L1)
	, L =< L1
	, string_front(W, L, S1, W1)
	, S1 = S
	, listSufix(W1, E, [S|Ls], C)
	.

extSufix(W, M, W, M).
extSufix(W, M, W1, [Ls|M]):-
	listSufix(W, W1, [], Ls).

extEnd(W, M, W, M).
extEnd(W, M, E, [e(En, attr(G, N))|M]):-
	end(En, G, N)
	, string_length(En, L)
	, string_length(W, L1)
	, L =< L1
	, string_front(W, L, E1, E)
	, E1 = En
	.
