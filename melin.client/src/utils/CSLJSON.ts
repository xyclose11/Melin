interface Author {
    given: string;
    family: string;
}

export interface CSLJSON {
    id: string;
    type: string;
    author?: Author[];
    title?: string;
    URL?: string;
    keyword?: string;
    ISBN?: string;
}
