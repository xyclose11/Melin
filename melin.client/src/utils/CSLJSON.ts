interface IContributor {
    given: string;
    family: string;
    "dropping-particle": string;
    "non-dropping-particle": string;
    suffix: string;
    "comma-suffix": string | number | boolean;
    "static-ordering": string | number | boolean;
    literal: string;
    "parse-names": string | number | boolean;
    additionalProperties: boolean;
}
// LEFT OFF ON CURATOR
export interface CSLJSON {
    id: string;
    type: string;
    author?: IContributor[];
    chair?: IContributor[];
    "collection-editor"?: IContributor[];
    compiler?: IContributor[];
    composer?: IContributor[];
    "container-author"?: IContributor[];
    contributor?: IContributor[];
    curator?: IContributor[];
    title?: string;
    URL?: string;
    keyword?: string;
    ISBN?: string;
    categories: string[];
    language: string;
    journalAbbreviation: string;
    shortTitle: string;
}
