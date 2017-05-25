# ImpRec - An Impact Recommender

ImpRec is a prototype tool supporting change impact analysis (CIA) developed as part of a research project at the Department of Computer Science at Lund University. The tool has been developed as a proof-of-concept of the potential benefits of reusing traceability from previously completed impact analyses. ImpRec is a recommendation system for software engineering (RSSE) that uses a combination of information retrieval-based trace recovery techniques and analysis of network structure to identify potential impact for a given tracker case.

- ImpRec has been evaluated in an industrial case study with two units of analysis in Sweden and India. The interview guides are available in the documentation folder.
- A  user manual is available in the Documentation folder. Note that ImpRec was released under the name CaseDigger in the industrial case study.

## The Semantic Network

The power of ImpRec lies in reusing knowledge captured in the semantic network. Unfortunatley, but the network from the publications is confidential information from a proprietary company. In order to use ImpRec, you thus need to extend the dummy data (from the Android project complemented by some fake relations) available in the "input" folder. The structure of the four files should be mostly self-explanatory.

- issueText.txt Contains the textual content of the issue reports, represented on three separate rows: 1) issue report ID, 2) title, and 3) full description.
- impactAnalyses.xml Represents the impact analysis reports attached to issue reports. This is the type of information that was parsed in the publications to capture previous impact from 10+ years of software evolution.
- semanticNetwork.xml Contains all relations between artifacts. Constructed from link mining in the issue repository and the historical impact analysis reports. First all impact items are listed, then all issue reports with their links to impact items and other issue reports. (Artifacts: <TrackerCase>, <REQ>, <TEST>, <MISC>, and <UNSPECIFIED>; Relations: <RelatedCase>, <SpecifiedBy>, <VerifiedBy>, <NeedsUpdate>, <ImpactedHW>, and <UnspecifiedLink>)
- translation.csv contains the titles of documents that might appear only as identifiers in the impact analysis reports (and thus in the semantic network).

## Primary Publications
- Borg, Wnuk, Regnell, and Runeson. Supporting Change Impact Analysis Using a Recommendation System: An Industrial Case Study in a Safety-Critical Context, IEEE Transactions on Software Engineering, DOI: 10.1109/TSE.2016.2620458, 2016.
- Borg and Runeson, Changes, Evolution and Bugs - Recommendation Systems for Issue Management, In Robillard, Maalej, Walker, and Zimmermann (Eds.), Recommendation Systems in Software Engineering, pp. 407-509, Springer, 2014.
- Borg, Gotel, and Wnuk. Enabling Traceability Reuse for Impact Analyses: A Feasibility Study in a Safety Context, In Proc. of the International Workshop on Traceability in Emerging Forms of Software Engineering, pp. 72-79, 2013.
