import { describe, expect, it } from "vitest";
import {
    parseDate,
    mapContributors,
} from "../../../utils/mapCSLToReference.ts";
import { DateVariable, NameVariable } from "../../../utils/CSLJSON.ts";
import { creatorTypes, ICreator } from "../../../utils/Reference.ts";

describe("ParseDate", () => {
    it("should correctly parse the date into a formatted EDTF level 0 string", () => {
        const dateVariable: DateVariable = {
            "date-parts": [[2025, 1, 3]],
        };
        expect(parseDate(dateVariable)).toBe("2025-1-3");
    });
});

describe("ParseDateOnlyGivenYear", () => {
    it("should correctly parse the date into a formatted EDTF level 0 string given invalid input", () => {
        const dateVariable: DateVariable = {
            "date-parts": [[2025]],
        };
        expect(parseDate(dateVariable)).toBe("2025");
    });
});

describe("ParseDateOnlyGivenYearAndMonth", () => {
    it("should correctly parse the date into a formatted EDTF level 0 string given invalid input", () => {
        const dateVariable: DateVariable = {
            "date-parts": [[2025, 4]],
        };
        expect(parseDate(dateVariable)).toBe("2025-4");
    });
});

describe("ParseDateGivenOutOfBoundMonth", () => {
    it("should return undefined when given a month > 12", () => {
        const dateVariable: DateVariable = {
            "date-parts": [[2025, 44]],
        };
        expect(parseDate(dateVariable)).toBe(undefined);
    });
});

describe("ParseDateGivenOutOfBoundMonth", () => {
    it("should return undefined when given a month < 1", () => {
        const dateVariable: DateVariable = {
            "date-parts": [[2025, 0]],
        };
        expect(parseDate(dateVariable)).toBe(undefined);
    });
});

describe("ParseDateGivenOutOfBoundDay", () => {
    it("should return undefined when given a day > 31", () => {
        const dateVariable: DateVariable = {
            "date-parts": [[2025, 4, 32]],
        };
        expect(parseDate(dateVariable)).toBe(undefined);
    });
});

describe("ParseDateGivenOutOfBoundDay", () => {
    it("should return undefined when given a day < 1", () => {
        const dateVariable: DateVariable = {
            "date-parts": [[2025, 4, 0]],
        };
        expect(parseDate(dateVariable)).toBe(undefined);
    });
});

describe("MapContributorsSuccessfullyMaps", () => {
    it("should return an array of ICreator[] type", () => {
        const contributors: NameVariable = {
            family: "Martin",
            given: "Rob",
        };

        const concreteAuthor: ICreator = {
            type: [creatorTypes["author"]],
            firstName: "Rob",
            lastName: "Martin",
        };
        expect(
            mapContributors(contributors, [creatorTypes.author]),
        ).toStrictEqual([concreteAuthor]);
    });
});
