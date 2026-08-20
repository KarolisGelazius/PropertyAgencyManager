# Real Estate Agency Analytics System

A system for multi-agency real estate analytics, property hierarchy modeling, and sorting via abstract data structures.

---

## Program User Manual

### Data Preparation
Prepare structured plain-text files (`.txt`) for each real estate agency. Each file represents an individual agency and its property catalog. 

Data values must follow a strict hierarchy and be separated by semicolons (`;`):
* **Line 1:** Agency Name
* **Line 2:** Agency Address
* **Line 3:** Contact Phone Number
* **Subsequent Lines:** Property listings starting with a type identifier (`Flat` or `House`), followed by geographical location, technical specifications, and subclass-specific attributes (floor number for flats, heating system type for houses).

**Example Agency File (`Agency1.txt`):**
```text
NT Partneriai
Vilniaus g. 10, Kaunas
+37060000000
Flat;Kaunas;Centras;Laisves al.;5;Mūrinis;2015;95.5;3;4
House;Kaunas;Panemune;Plento g.;12;Mūrinis;2020;215.0;5;Dujinis
