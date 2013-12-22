%% list_front(source, len, first, end); разделяет входящий список на два начиная с начала
list_front([], _, [], []) :-!.

list_front([H|L], 1, [H], L) :-!.

list_front([H|T], N, [H|F], R) :-
	N1 is N - 1
	, list_front(T, N1, F, R)
	.

%% выдает в F строку из Str длиной L в E остаток от строки Str
% для весии >= 6.5 надо использовать string_chars
string_front(Str, L, F, E) :-
	string_to_list(Str, Chs)
	, list_front(Chs, L, Fs, Es)
	, string_to_list(F, Fs) 
	, string_to_list(E, Es)
	.

rev(L, R):-
	rev(L, [], R).

rev([], L, L).
rev([H|T], L, R) :-
	rev(T, [H|L], R).

core("уч").
core("нес").
core("нёс").

prefix("вы").
prefix("из").
prefix("об").
prefix("за").

suffix("и").
suffix("л").
suffix("ак").

end("а", gen('женский'), num('ед')).
end("о", gen('средний'), num('ед')).
end("и", gen('нет'), num('мн')).

getMorf(L) :-
	rev(L, L1),
	atomic_list_concat(L1, ',', R),
	write('morf('), write(R), write(')')
	.

find(W, morf(M1, M2, M3, M4)):-
%find(W, M5):-
	extPrefix(W, W1, M1)
%	, M1 = [H1|_]
	, extCore(W1, W2, M2)
%	, M2 = [H2|_]
	, extSuffix(W2, W3, M3)
%	, M3 = [H3|_]
	, extEnd(W3, E, M4)
%	, M4 = [H4|_] 
	, string_length(E, L)
	, L < 1
%	, rev(M4, M5)
	, !
	.

extPrefix(W, W, prefix).
%extPrefix(W, M, E, [prefix(R)|M]):-
extPrefix(W, E, prefix(R)):-
	prefix(P)
	, string_to_list(R, P) % только для v6.2
	, string_length(R, L)
	, string_length(W, L1)
	, L =< L1
	, string_front(W, L, P1, E)
	, P1 = R
	.

%extCore(W, M, E, [core(R)|M]):-
extCore(W, E, core(R)):-
	core(C)
	, string_to_list(R, C) % только для v6.2
	, string_length(C, L)
	, string_length(W, L1)
	, L =< L1
	, string_front(W, L, C1, E)
	, C1 = R
	.

listSuffix(W, W, Ls, suffix(Ls)).
listSuffix(W, E, Ls, C) :-
	suffix(S)
	, string_to_list(R, S) % только для v6.2
	, string_length(S, L)
	, string_length(W, L1)
	, L =< L1
	, string_front(W, L, S1, W1)
	, S1 = R
	, listSuffix(W1, E, [R|Ls], C)
%	, rev(Ls, Lsr)	
	.

extSuffix(W, W, suffix).
%extSuffix(W, M, W1, [Ls|M]):-
extSuffix(W, W1, Ls):-
	listSuffix(W, W1, [], Ls).

extEnd(W, W, end(attr(gen('мужской'), num('ед')))).
%extEnd(W, M, W, [end(attr(gen('мужской'), num('ед')))|M]).
%extEnd(W, M, E, [end(R, attr(G, N))|M]):-
extEnd(W, E, end(R, attr(G, N))):-
	end(En, G, N)
	, string_to_list(R, En) % только для v6.2
	, string_length(En, L)
	, string_length(W, L1)
	, L =< L1
	, string_front(W, L, E1, E)
	, E1 = R
	.
