enum Papel {
    Visto = 0,
    Aprovacao = 1
};
export interface User {
    id: string;
    login: string;
    senha: string;
    papel: Papel;
}
