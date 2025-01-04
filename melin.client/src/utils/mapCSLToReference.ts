import { CSLJSON, DateVariable, NameVariable } from "../utils/CSLJSON.ts";
import { creatorTypes, ICreator, IReference } from "../utils/Reference.ts";

export function mapCSLToReference(csl: CSLJSON) {
    const type = csl.type.toLowerCase();
    if (!type) {
        throw new Error("Type is required when mapping CSL to Reference");
    }

    const convertedData: IReference = {
        URL: csl.URL,
        abstractNote: csl.abstract,
        accessed:
            csl.accessed !== undefined ? parseDate(csl.accessed) : undefined,
        applicationNumber: "",
        archive: csl.archive,
        archiveLocation: csl.archive_location,
        assignee: "",
        audioRecordingFormat: "",
        billNumber: "",
        blogTitle: "",
        bookTitle: "",
        callNumber: csl["call-number"],
        caseName: "",
        citationKey: csl["citation-key"],
        code: "",
        codePages: "",
        codeVolume: "",
        committee: "",
        company: "",
        conferenceName: "",
        country: "",
        court: "",
        creators:
            csl.author !== undefined
                ? mapContributors(csl.author, [creatorTypes["author"]])
                : undefined,
        dataType: "",
        date: "",
        dateDecided: "",
        datePublished:
            csl.issued !== undefined ? parseDate(csl.issued) : undefined,
        dimensions: csl.dimensions,
        distributor: "",
        docketNumber: "",
        documentNumber: "",
        doi: csl.DOI,
        edition: csl.edition?.toString(),
        episodeNumber: "",
        extraFields: [],
        fileFormat: "",
        filingDate: "",
        firstPage: "",
        format: "",
        genre: csl.genre,
        history: "",
        identifier: "",
        isbn: csl.ISBN,
        issn: csl.ISSN,
        issue: csl.issue?.toString(),
        issueDate: "",
        issuingAuthority: csl.authority,
        journalAbbr: csl.journalAbbreviation,
        label: csl["citation-label"],
        language: csl.language,
        legalStatus: csl.status,
        legislativeBody: "",
        libraryCatalog: "",
        locationStored: "",
        manuscriptType: "",
        mapType: "",
        medium: csl.medium,
        numberOfPages: csl["number-of-pages"]?.toString(),
        numberOfVolumes: 0,
        pageAmount: 0,
        pages: csl.page?.toString(),
        patentNumber: "",
        place: "",
        postType: "",
        priorityNumber: "",
        proceedingsTitle: "",
        programTitle: "",
        programmingLanguage: "",
        publication: csl.publisher,
        publisher: "",
        references: [],
        reporter: "",
        reporterVolume: "",
        repository: "",
        repositoryLocation: "",
        rights: [],
        runningTime: "",
        scale: "",
        section: "",
        series: "",
        seriesNumber: 0,
        seriesTitle: "",
        session: "",
        subject: "",
        system: "",
        versionNumber: "",
        videoRecordingFormat: "",
        volume: csl.volume?.toString(),
        volumeAmount: 0,
        websiteType: "",
        type: csl.type,
        title: csl.title ?? "",
    };

    return convertedData;
}

export function mapContributors(
    csljson: NameVariable[],
    type: creatorTypes[],
): ICreator[] {
    let contributors: NameVariable[] = [];

    if (csljson !== undefined) {
        contributors.push(...(Array.isArray(csljson) ? csljson : [csljson]));
    }

    if (!contributors) return [];
    return contributors.map((contributor) => ({
        type: type,
        firstName: contributor.given || "",
        lastName: contributor.family || "",
    }));
}
// function parsePages(page: string): number | undefined {
//     const pageNumber = parseInt(page, 10);
//     return isNaN(pageNumber) ? undefined : pageNumber;
// }

// Following EDTF Level 0 standard
// Requires that YYYY always be present
// TODO ADD COVERAGE FOR OTHER DATE VARIABLE TYPES
export function parseDate(date: DateVariable): string | undefined {
    if (!date) {
        return undefined;
    }
    let year, month, day;

    if (
        date["date-parts"] !== undefined &&
        date["date-parts"][0] !== undefined
    ) {
        const d = date["date-parts"][0];
        year = d[0];
        month = d[1];
        day = d[2];
    }

    if (year === undefined) {
        return undefined;
    }

    if (month === undefined) {
        return year.toString();
    }

    if (parseInt(month.toString()) > 12 || parseInt(month.toString()) < 1) {
        return undefined;
    }

    if (day === undefined) {
        return year + "-" + month;
    }

    if (parseInt(day.toString()) > 31 || parseInt(day.toString()) < 1) {
        return undefined;
    }

    return year + "/" + month + "/" + day;
}
