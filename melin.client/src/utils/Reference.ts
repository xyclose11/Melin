// export interface IReference {
//     type: string;
//     title: string;
// }

// TODO MAKE THIS INCLUDE EVERY BACKEND MODEL REFERENCE FIELD OPTIONAL
export enum creatorTypes {
    author,
    editor,
    series_editor,
    translator,
    reviewed_author,
    artist,
    performer,
    composer,
    director,
    podcaster,
    cartographer,
    programmer,
    presenter,
    interview_with,
    interviewer,
    recipient,
    sponsor,
    inventor,
}

export interface ICreator {
    type: creatorTypes[];
    firstName: string;
    lastName: string;
}

export interface IReference {
    // SHARED FIELDS
    type: string;
    title: string;
    creators?: ICreator[];
    abstractNote?: string;
    language?: string;
    datePublished: Date;
    rights?: string[];
    extraFields?: string[];
    accessed?: string;
    locationStored?: string;
    archive?: string;
    archiveLocation?: string;
    libraryCatalog?: string;
    callNumber?: string;
    URL?: string;

    // SPECIFIC FIELDS BELOW
    medium?: string; // For Artwork, PrimarySource
    dimensions?: string; // For Artwork
    scale?: string; // For Artwork, Map
    mapType?: string; // For Artwork, Map
    audioRecordingFormat?: string; // For AudioRecording, RadioBroadcast
    seriesTitle?: string; // Common
    volume?: string; // Common
    numberOfVolumes?: number; // Common
    place?: string; // Common
    label?: string; // For AudioRecording
    runningTime?: string; // For AudioRecording, Film, Podcast, RadioBroadcast, Recording
    billNumber?: string; // For Bill, Legislation
    code?: string; // For Bill, Legislation
    codeVolume?: string; // For Bill, Legislation
    section?: string; // Common
    codePages?: string; // For Bill
    legislativeBody?: string; // For Bill, Hearing, Legislation
    session?: string; // For Bill, Legislation
    history?: string; // For Bill, LegalCases, Hearing, Legislation
    blogTitle?: string; // For BlogPost
    websiteType?: string; // For BlogPost
    publication?: string; // For Book
    bookTitle?: string; // For Book, BookSection
    issue?: string; // For Book, JournalArticle
    pages?: string; // Common
    edition?: string; // Common
    series?: string; // Common
    seriesNumber?: number; // Common
    volumeAmount?: number; // For Book
    pageAmount?: number; // For Book
    publisher?: string; // Common
    journalAbbr?: string; // For Book
    isbn?: string; // For Common
    issn?: string; // For Common
    proceedingsTitle?: string; // For ConferencePaper
    conferenceName?: string; // For ConferencePaper
    doi?: string; // For ConferencePaper, Dataset, JournalArticle
    identifier?: string; // For Dataset
    dataType?: string; // For Dataset
    versionNumber?: string; // For Dataset, Software
    repository?: string; // For Dataset
    repositoryLocation?: string; // For Dataset
    format?: string; // For Dataset, Film
    citationKey?: string; // For Dataset
    subject?: string; // For PrimarySource, Email
    date?: string; // Common
    distributor?: string; // For Film
    genre?: string; // For Film
    videoRecordingFormat?: string; // For Film
    postType?: string; // For ForumPost
    committee?: string; // For Hearing, Legislation
    documentNumber?: string; // For Hearing, Legislation
    caseName?: string; // For LegalCases
    court?: string; // For LegalCases
    dateDecided?: string; // For LegalCases
    docketNumber?: string; // For LegalCases
    reporter?: string; // For LegalCases
    reporterVolume?: string; // For LegalCases
    firstPage?: string; // For LegalCases
    country?: string; // For Patent
    assignee?: string; // For Patent
    issuingAuthority?: string; // For Patent
    patentNumber?: string; // For Patent
    filingDate?: string; // For Patent
    issueDate?: string; // For Patent
    applicationNumber?: string; // For Patent
    priorityNumber?: string; // For Patent
    references?: string[]; // For Patent
    legalStatus?: string; // For Patent
    manuscriptType?: string; // For Manuscript
    numberOfPages?: string; // For Manuscript
    programTitle?: string; // For RadioBroadcast, Recording
    episodeNumber?: string; // For RadioBroadcast, Recording
    system?: string; // For Software
    company?: string; // For Software
    programmingLanguage?: string; // For Software
    fileFormat?: string; // For Recording
}
