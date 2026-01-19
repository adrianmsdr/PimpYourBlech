using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PimpYourBlech_Data.Migrations
{
    /// <inheritdoc />
    public partial class Seeding2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DatePermit", "DateProduction", "Price" },
                values: new object[] { "01/2026", "10/2025", 44999.99m });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DatePermit", "DateProduction", "Price" },
                values: new object[] { "01/2021", "11/2020", 24999.99m });

            migrationBuilder.UpdateData(
                table: "OrderPositions",
                keyColumn: "OrderPositionId",
                keyValue: 1,
                column: "UnitPrice",
                value: 999.99m);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1,
                columns: new[] { "Description", "Price" },
                values: new object[] { "Die Queenstown Felge steht für sportliches Design und solide Verarbeitung. Mit einem Durchmesser von 19 Zoll und einer Breite von 8 Zoll verleiht sie dem Fahrzeug eine kraftvolle, moderne Optik, ohne dabei übertrieben zu wirken. Das markante Mehrspeichen-Design sorgt für einen dynamischen Auftritt und lässt die Bremsanlage optisch größer und präsenter erscheinen.\n\nGefertigt für Volkswagen-Fahrzeuge, verbindet diese Felge Alltagstauglichkeit mit einem klaren Performance-Look. Sie eignet sich sowohl für den täglichen Einsatz als auch für Fahrer, die ihrem Fahrzeug eine deutlich aufgewertete Erscheinung verleihen wollen, ohne ins Extreme zu gehen.\n\nDank der ausgewogenen Dimensionen bietet die Queenstown Felge ein gutes Verhältnis aus Stabilität, Fahrkomfort und sportlicher Straßenlage. Eine ideale Wahl für alle, die Wert auf Qualität, saubere Optik und eine stimmige Gesamtwirkung legen.", 999.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2,
                columns: new[] { "Description", "Price" },
                values: new object[] { "Die Warmenau Performance Felge richtet sich klar an Fahrer, die keine Kompromisse wollen. Mit einem großzügigen Durchmesser von 20 Zoll und einer Breite von 8,5 Zoll setzt sie ein deutlich sportliches Statement und unterstreicht den Performance-Charakter des Fahrzeugs schon im Stand. Das präzise, kantige Speichendesign wirkt technisch, aggressiv und hochwertig zugleich.\n\nEntwickelt für Volkswagen-Fahrzeuge, verbindet diese Felge modernes Motorsport-Design mit hoher Alltagstauglichkeit. Die klare Linienführung sorgt für eine starke Tiefenwirkung und bringt besonders bei dunklen Fahrzeugfarben ihre volle Wirkung zur Geltung.\n\nDurch die breitere Auslegung bietet die Warmenau Performance Felge eine verbesserte Straßenlage und ein direkteres Fahrgefühl. Sie ist die richtige Wahl für alle, die Optik und Fahrdynamik gezielt aufwerten wollen und ihrem Fahrzeug einen kompromisslosen Performance-Look verleihen möchten.", 1199.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3,
                columns: new[] { "Description", "Price" },
                values: new object[] { "Der GTI 2.0 TFSI Performance Motor steht für kompromisslose Leistung und moderne Volkswagen-Performance-Technologie. Mit 325 PS (239 kW) aus 2,0 Litern Hubraum liefert dieser Benzinmotor eine beeindruckende Kombination aus Durchzugskraft, Effizienz und sportlichem Charakter. Entwickelt für Fahrer, die maximale Performance erwarten – ohne Alltags­tauglichkeit einzubüßen.\n\nIn Verbindung mit dem 6-Gang-Automatikgetriebe sorgt der Motor für schnelle, präzise Gangwechsel und eine direkte Kraftentfaltung. Das Ergebnis ist ein dynamisches Fahrerlebnis mit souveräner Beschleunigung, hoher Laufruhe und klarer Kontrolle in jeder Fahrsituation.\n\nDer GTI 2.0 TFSI Performance Motor ist die ideale Wahl für sportlich ambitionierte Fahrer, die ein kraftvolles Upgrade suchen. Ob auf der Straße oder bei engagierter Fahrweise – dieser Motor liefert genau das, was der Name verspricht: echte GTI-Performance auf hohem technischen Niveau.", 2499.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4,
                columns: new[] { "Description", "Price" },
                values: new object[] { "Der GTI Clubsport RS Motor markiert die Spitze der Volkswagen-Performanceklasse. Mit 360 PS (265 kW) aus 2,0 Litern Hubraum liefert dieser Benzinmotor eine kompromisslose Leistungsentfaltung, die klar auf sportliche Höchstansprüche ausgelegt ist. Dieses Aggregat richtet sich an Fahrer, die maximale Dynamik und ein spürbar aggressiveres Fahrgefühl suchen.\n\nDas 6-Gang-Automatikgetriebe sorgt für extrem schnelle Schaltvorgänge und eine direkte Umsetzung der Motorleistung auf die Straße. Die Kraftentfaltung erfolgt explosiv, gleichzeitig kontrolliert und präzise – ideal für ambitionierte Fahrweise und performanceorientierte Fahrzeugkonzepte.\n\nDer GTI Clubsport RS Motor ist kein Komfort-Upgrade, sondern ein echtes Performance-Statement. Entwickelt für Enthusiasten, die das Maximum aus ihrem Fahrzeug herausholen wollen und bewusst auf kompromisslose Leistung setzen.", 3399.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 5,
                columns: new[] { "Description", "Price" },
                values: new object[] { "Das IQ.Light LED Matrix Pro steht für modernste Lichttechnologie und maximale Sicherheit bei jeder Fahrbedingung. Mit einer Lichtleistung von 3.200 Lumen sorgt dieses LED-System für eine außergewöhnlich helle und gleichmäßige Ausleuchtung der Fahrbahn – ohne andere Verkehrsteilnehmer zu blenden. Die präzise Matrix-Technik passt den Lichtkegel intelligent an die Umgebung an.\n\nDank der vollwertigen LED-Technologie überzeugt das System durch schnelle Reaktionszeiten, hohe Energieeffizienz und eine deutlich längere Lebensdauer gegenüber herkömmlichen Scheinwerfern. Besonders bei Nachtfahrten, auf Landstraßen oder bei schlechten Sichtverhältnissen bietet IQ.Light einen spürbaren Sicherheitsgewinn.\n\nDas IQ.Light LED Matrix Pro ist die ideale Wahl für Fahrer, die höchsten Wert auf Sicht, Sicherheit und moderne Fahrzeugtechnik legen. Ein Premium-Upgrade, das Funktionalität und Hightech-Design perfekt miteinander verbindet.", 1449.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 6,
                columns: new[] { "Description", "Price" },
                values: new object[] { "Das Dynamic Vision LED Blackline vereint moderne LED-Lichttechnik mit einer markant sportlichen Optik. Mit einer Lichtleistung von 3.000 Lumen sorgt dieses System für eine klare, gleichmäßige Ausleuchtung der Fahrbahn und verbessert die Sicht bei Nacht sowie bei schlechten Wetterbedingungen deutlich. Gleichzeitig verleiht das dunkle Blackline-Design dem Fahrzeug einen kraftvollen, hochwertigen Look.\n\nDie LED-Technologie bietet schnelle Reaktionszeiten, hohe Energieeffizienz und eine lange Lebensdauer. Das Lichtbild ist präzise abgestimmt und unterstützt sicheres Fahren, ohne andere Verkehrsteilnehmer unnötig zu blenden. Besonders auf Landstraßen und im Stadtverkehr zeigt sich der Vorteil der gleichmäßigen Ausleuchtung.\n\nDas Dynamic Vision LED Blackline ist die richtige Wahl für Fahrer, die Funktionalität und sportliches Design kombinieren möchten. Ein stilvolles Upgrade, das Sicherheit, moderne Technik und eine ausdrucksstarke Optik miteinander verbindet.", 1579.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 7,
                column: "Price",
                value: 1799.99m);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 8,
                column: "Price",
                value: 1899.99m);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 9,
                column: "Price",
                value: 1599.99m);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 10,
                column: "Price",
                value: 1699.99m);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 11,
                columns: new[] { "Description", "Price" },
                values: new object[] { "Der Polo 1.0 TSI BlueMotion Motor ist auf Effizienz, Zuverlässigkeit und Alltagstauglichkeit ausgelegt. Mit 110 PS (81 kW) aus 1,0 Liter Hubraum bietet dieser Benzinmotor eine überraschend agile Leistungsentfaltung bei gleichzeitig niedrigem Verbrauch. Die BlueMotion-Technologie steht dabei für optimierte Effizienz und reduzierte Emissionen im täglichen Fahrbetrieb.\n\nIn Kombination mit dem 6-Gang-Schaltgetriebe ermöglicht der Motor eine direkte, kontrollierte Kraftübertragung und ein bewusst aktives Fahrerlebnis. Besonders im Stadt- und Pendelverkehr überzeugt das Aggregat durch seine Laufruhe, gute Elastizität und wirtschaftliche Charakteristik.\n\nDer Polo 1.0 TSI BlueMotion Motor ist die ideale Wahl für Fahrer, die ein zuverlässiges und sparsames Antriebskonzept suchen, ohne auf moderne Technik und solide Fahrleistungen verzichten zu wollen. Funktional, effizient und perfekt für den Alltag.", 1799.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 12,
                columns: new[] { "Description", "Price" },
                values: new object[] { "Der Polo 1.5 TSI GT-Line Motor bietet eine sportlich abgestimmte Leistungsreserve bei gleichzeitig hoher Effizienz. Mit 150 PS (110 kW) aus 1,5 Litern Hubraum positioniert sich dieser Benzinmotor deutlich über den klassischen Alltagsaggregaten und verleiht dem Polo ein spürbar dynamischeres Fahrverhalten.\n\nIn Kombination mit dem 6-Gang-Automatikgetriebe überzeugt der Motor durch schnelle, saubere Gangwechsel und eine gleichmäßige Kraftentfaltung. Beschleunigung, Durchzug und Laufruhe sind ausgewogen abgestimmt und machen den Polo sowohl im Stadtverkehr als auch auf der Autobahn souverän und agil.\n\nDer Polo 1.5 TSI GT-Line Motor richtet sich an Fahrer, die mehr Leistung und Sportlichkeit erwarten, ohne auf Komfort und Alltagstauglichkeit zu verzichten. Ein ideales Upgrade für alle, die den Polo deutlich dynamischer erleben möchten..", 2399.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 13,
                columns: new[] { "Description", "Price" },
                values: new object[] { "Das Polo LED Comfort Beam ist auf angenehme Ausleuchtung und zuverlässige Alltagstauglichkeit ausgelegt. Mit einer Lichtleistung von 2.600 Lumen bietet dieses LED-System ein ausgewogenes, gleichmäßiges Lichtbild, das für gute Sicht bei Nacht und in der Dämmerung sorgt, ohne dabei zu blenden oder zu ermüden.\n\nDie moderne LED-Technologie garantiert eine hohe Energieeffizienz, lange Lebensdauer und sofortige Helligkeit beim Einschalten. Besonders im Stadtverkehr und auf täglichen Pendelstrecken überzeugt das Polo LED Comfort Beam durch seine ruhige Lichtcharakteristik und den spürbaren Komfortgewinn.\n\nDas Polo LED Comfort Beam ist die ideale Wahl für Fahrer, die ein zuverlässiges, komfortorientiertes Lichtsystem suchen. Funktional, effizient und perfekt auf den Alltag abgestimmt.", 979.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 14,
                columns: new[] { "Description", "Price" },
                values: new object[] { "Das Polo LED NightVision Plus wurde für verbesserte Sicht und erhöhte Sicherheit bei Dunkelheit entwickelt. Mit einer Lichtleistung von 3.100 Lumen bietet dieses LED-System eine starke, gleichmäßige Ausleuchtung der Fahrbahn und erleichtert das Erkennen von Hindernissen, Fußgängern und Verkehrszeichen bei Nacht.\n\nDie moderne LED-Technologie sorgt für eine hohe Energieeffizienz, sofortige volle Helligkeit und eine lange Lebensdauer. Das präzise abgestimmte Lichtbild reduziert Ermüdung bei längeren Nachtfahrten und bietet ein spürbares Plus an Fahrkomfort, besonders auf schlecht beleuchteten Straßen.\n\nDas Polo LED NightVision Plus ist die ideale Wahl für Fahrer, die Wert auf Sicherheit, klare Sicht und moderne Lichttechnik legen. Ein hochwertiges Upgrade für souveränes und entspanntes Fahren bei Nacht.", 1149.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 15,
                columns: new[] { "Description", "Price" },
                values: new object[] { "Der GTI EcoBoost Hybrid Motor verbindet sportliche GTI-DNA mit moderner Hybridtechnologie. Mit 280 PS (210 kW) aus 1,8 Litern Hubraum bietet dieses Aggregat eine ausgewogene Kombination aus Leistung, Effizienz und zukunftsorientierter Antriebstechnik. Der Hybridantrieb sorgt für kraftvollen Durchzug bei gleichzeitig reduzierten Emissionen und verbessertem Verbrauch.\n\nIn Verbindung mit dem 6-Gang-Automatikgetriebe wird die Leistung gleichmäßig, direkt und komfortabel auf die Straße übertragen. Der elektrische Anteil unterstützt den Verbrennungsmotor besonders beim Anfahren und Beschleunigen, was zu einem spontanen, dynamischen Fahrgefühl führt – ohne die typische GTI-Sportlichkeit zu verlieren.\n\nDer GTI EcoBoost Hybrid Motor richtet sich an Fahrer, die Performance genießen wollen, dabei aber Wert auf Effizienz und moderne Technologie legen. Ein intelligentes Performance-Upgrade für alle, die sportliches Fahren mit einem Blick in die Zukunft verbinden möchten.", 3899.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 16,
                columns: new[] { "Description", "Price" },
                values: new object[] { "Das NightDrive LED UltraBeam ist auf maximale Sichtleistung und höchste Sicherheit bei Nachtfahrten ausgelegt. Mit einer beeindruckenden Lichtleistung von 3.800 Lumen sorgt dieses LED-System für eine extrem helle, weitreichende Ausleuchtung der Fahrbahn und ermöglicht frühzeitiges Erkennen von Hindernissen, Fahrbahnmarkierungen und Verkehrszeichen.\n\nDank moderner LED-Technologie überzeugt das NightDrive UltraBeam durch ein präzises, klares Lichtbild, hohe Energieeffizienz und eine lange Lebensdauer. Besonders auf dunklen Landstraßen und bei schlechten Sichtverhältnissen spielt dieses System seine Stärken aus und bietet ein deutliches Plus an Sicherheit und Fahrkomfort.\n\nDas NightDrive LED UltraBeam richtet sich an Fahrer, die keine Kompromisse bei Sicht und Sicherheit eingehen möchten. Ein leistungsstarkes Premium-Upgrade für maximale Kontrolle und Vertrauen bei jeder Nachtfahrt.", 1749.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 17,
                column: "Price",
                value: 2099.99m);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 18,
                column: "Price",
                value: 1649.99m);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 19,
                columns: new[] { "Description", "Price" },
                values: new object[] { "Der Polo 1.2 Hybrid Drive Motor ist konsequent auf Effizienz, Alltagstauglichkeit und moderne Antriebstechnologie ausgelegt. Mit 130 PS (96 kW) aus 1,2 Litern Hubraum kombiniert dieses Aggregat einen Benzinmotor mit Hybridunterstützung und bietet damit ein ausgewogenes Verhältnis aus Leistung, Verbrauch und Komfort.\n\nDas 6-Gang-Automatikgetriebe sorgt für sanfte, nahezu unmerkliche Gangwechsel und eine gleichmäßige Kraftentfaltung. Der Hybridantrieb unterstützt den Verbrennungsmotor besonders bei niedrigen Drehzahlen und im Stadtverkehr, was zu einem ruhigen Fahrgefühl, besserer Effizienz und reduziertem Verbrauch führt.\n\nDer Polo 1.2 Hybrid Drive Motor richtet sich an Fahrer, die ein modernes, sparsames Antriebskonzept suchen, ohne auf ausreichende Leistungsreserven zu verzichten. Eine clevere Wahl für den täglichen Einsatz mit Fokus auf Wirtschaftlichkeit und zeitgemäße Technik.", 2599.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 20,
                columns: new[] { "Description", "Price" },
                values: new object[] { "Das Polo Adaptive Pixel Light repräsentiert die fortschrittlichste Lichttechnologie in der Polo-Klasse. Mit einer Lichtleistung von 3.400 Lumen und adaptiver Pixelsteuerung passt dieses LED-System den Lichtkegel präzise an Fahrbahn, Verkehr und Umgebung an. Das Ergebnis ist maximale Sicht bei gleichzeitig minimaler Blendung anderer Verkehrsteilnehmer.\n\nDie intelligente Pixel-LED-Technologie ermöglicht eine dynamische Ausleuchtung, die sich in Echtzeit an Geschwindigkeit, Lenkwinkel und Verkehrssituation anpasst. Besonders bei Nachtfahrten, auf kurvigen Strecken oder bei schlechten Wetterbedingungen bietet dieses System einen deutlichen Sicherheits- und Komfortgewinn.\n\nDas Polo Adaptive Pixel Light ist die ideale Wahl für Fahrer, die modernste Lichttechnik, höchste Sicherheit und Premium-Komfort erwarten. Ein High-End-Upgrade für souveränes Fahren bei jeder Lichtsituation.", 1349.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 21,
                columns: new[] { "Description", "Price" },
                values: new object[] { "Die Pretoria Sport Felge kombiniert ein reduziertes, sportliches Design mit hoher Alltagstauglichkeit. Mit einem Durchmesser von 18 Zoll und einer Breite von 7,5 Zoll ist sie bewusst ausgewogen dimensioniert und eignet sich ideal für Fahrer, die sportliche Optik wollen, ohne Komfort und Effizienz zu opfern. Das klare Speichendesign wirkt leicht, modern und zeitlos.\n\nAls Volkswagen Original-Design fügt sich die Pretoria Sport Felge harmonisch in die Linienführung des Fahrzeugs ein. Sie verleiht dem Auto eine dezente, aber spürbare Aufwertung und passt sowohl zu sportlichen als auch zu eleganten Fahrzeugkonfigurationen.\n\nDank der moderaten Größe bietet diese Felge ein angenehmes Fahrverhalten, gute Alltagseigenschaften und eine zuverlässige Straßenlage. Die Pretoria Sport Felge ist damit die perfekte Wahl für Fahrer, die eine sportliche, aber vernünftige Lösung suchen.", 1349.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 22,
                columns: new[] { "Description", "Price" },
                values: new object[] { "Die Astana Felge ist auf maximale Alltagstauglichkeit und zeitloses Design ausgelegt. Mit einem Durchmesser von 16 Zoll und einer Breite von 6,5 Zoll bietet sie eine komfortorientierte Lösung für den täglichen Einsatz. Das geschlossene, aerodynamisch wirkende Design sorgt für eine ruhige, saubere Optik und fügt sich unauffällig in das Gesamtbild des Fahrzeugs ein.\n\nAls Volkswagen Originalfelge wurde die Astana speziell für Effizienz, Langlebigkeit und Fahrkomfort entwickelt. Sie eignet sich ideal für Stadtverkehr, Langstrecken und den ganzjährigen Einsatz. Die ausgewogene Größe unterstützt ein angenehmes Abrollverhalten und reduziert den Verschleiß von Reifen und Fahrwerk.\n\nDie Astana Felge ist die richtige Wahl für Fahrer, die Wert auf Zuverlässigkeit, Komfort und ein dezentes Erscheinungsbild legen – funktional, unaufdringlich und konsequent alltagstauglich.", 849.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 23,
                columns: new[] { "Description", "Price" },
                values: new object[] { "Die Bergamo Sport Felge verbindet sportliche Eleganz mit hoher Alltagstauglichkeit. Mit einem Durchmesser von 17 Zoll und einer Breite von 7,0 Zoll bietet sie einen ausgewogenen Kompromiss zwischen dynamischer Optik und komfortablem Fahrverhalten. Das geschwungene, kontrastreiche Speichendesign verleiht dem Fahrzeug eine moderne und hochwertige Ausstrahlung.\n\nAls Volkswagen Originalfelge ist die Bergamo Sport Felge perfekt auf die Fahrzeuge des Herstellers abgestimmt. Sie fügt sich harmonisch in das Gesamtbild ein und wertet sowohl kompakte Modelle als auch Mittelklassefahrzeuge sichtbar auf, ohne dabei aufdringlich zu wirken.\n\nDurch ihre moderate Dimension sorgt die Felge für ein stabiles Fahrgefühl, gute Effizienz und angenehmen Komfort im Alltag. Die Bergamo Sport Felge ist ideal für Fahrer, die sportliche Akzente setzen möchten, dabei aber nicht auf Alltagstkomfort verzichten wollen.", 979.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 24,
                columns: new[] { "Description", "Price" },
                values: new object[] { "Die Verona Black Performance Felge steht für einen kompromisslos sportlichen Auftritt mit klarer, kraftvoller Linienführung. Mit einem Durchmesser von 18 Zoll und einer Breite von 7,5 Zoll bietet sie eine ideale Balance aus Performance, Stabilität und Alltagstauglichkeit. Das kontrastreiche Schwarz-Design mit markanten Speichen sorgt für eine aggressive, moderne Optik und unterstreicht den sportlichen Charakter des Fahrzeugs.\n\nAls Volkswagen Performance Felge ist die Verona Black perfekt auf die technischen Anforderungen des Herstellers abgestimmt. Sie fügt sich nahtlos in sportliche Fahrzeugkonzepte ein und wirkt besonders stark bei dunklen Lackierungen, wo sie einen klaren Performance-Akzent setzt.\n\nDie ausgewogene Dimensionierung sorgt für präzises Lenkverhalten, gute Straßenlage und zuverlässigen Fahrkomfort. Die Verona Black Performance Felge ist die richtige Wahl für Fahrer, die ein sportliches Erscheinungsbild mit funktionaler Alltagstauglichkeit verbinden möchten.", 1099.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 30,
                column: "Description",
                value: "Der GTI Carbon Heckspoiler setzt ein klares sportliches Statement und unterstreicht den Performance-Charakter des Fahrzeugs bereits auf den ersten Blick. Gefertigt aus hochwertigem Carbon, überzeugt der Spoiler durch sein geringes Gewicht, hohe Stabilität und eine edle, motorsportnahe Optik.\n\nNeben der visuellen Aufwertung trägt der Heckspoiler zu einer verbesserten Aerodynamik bei, indem er den Abtrieb an der Hinterachse optimiert und das Fahrzeug bei höheren Geschwindigkeiten stabiler auf der Straße hält. Die präzise Formgebung fügt sich harmonisch in das Fahrzeugdesign ein und ergänzt die typische GTI-Linie perfekt.\n\nDer GTI Carbon Heckspoiler ist die ideale Wahl für Fahrer, die sportliche Optik und funktionalen Nutzen kombinieren möchten. Ein hochwertiges Performance-Upgrade, das Design, Technik und Fahrdynamik gekonnt miteinander verbindet.");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 31,
                column: "Description",
                value: "Der Polo Sport Heckspoiler verleiht dem Polo eine dynamische, sportliche Optik und setzt gezielte Akzente am Heck des Fahrzeugs. Mit seiner kompakten, klaren Form fügt sich der Spoiler harmonisch in das Gesamtbild ein und unterstreicht den sportlichen Charakter, ohne überladen zu wirken.\n\nNeben der optischen Aufwertung unterstützt der Heckspoiler die Aerodynamik, indem er den Luftstrom am Fahrzeugheck sauberer führt und bei höheren Geschwindigkeiten für zusätzliche Stabilität sorgt. Die passgenaue Ausführung gewährleistet eine perfekte Integration in das originale Fahrzeugdesign.\n\nDer Polo Sport Heckspoiler ist ideal für Fahrer, die ihrem Fahrzeug einen sportlichen Look verleihen möchten, dabei aber Wert auf Alltagstauglichkeit und dezentes Design legen. Ein ausgewogenes Upgrade für Stil und Funktion.");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DatePermit", "DateProduction", "Price" },
                values: new object[] { "2026", "2025", 45000m });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DatePermit", "DateProduction", "Price" },
                values: new object[] { "2021", "2020", 25000m });

            migrationBuilder.UpdateData(
                table: "OrderPositions",
                keyColumn: "OrderPositionId",
                keyValue: 1,
                column: "UnitPrice",
                value: 1000m);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1,
                columns: new[] { "Description", "Price" },
                values: new object[] { "Sportliche Alufelge mit roten Akzenten.", 1000m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2,
                columns: new[] { "Description", "Price" },
                values: new object[] { "Aerodynamische Premium-Felge für sportliche Fahrweise.", 1200m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3,
                columns: new[] { "Description", "Price" },
                values: new object[] { "325 PS Turbo-Benzinmotor.", 2500m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4,
                columns: new[] { "Description", "Price" },
                values: new object[] { "Hochleistungsmotor mit Rennsportabstimmung.", 3400m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 5,
                columns: new[] { "Description", "Price" },
                values: new object[] { "Adaptive Matrix-LED-Scheinwerfer.", 1450m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 6,
                columns: new[] { "Description", "Price" },
                values: new object[] { "Sportlicher LED-Scheinwerfer mit dunklem Gehäuse.", 1580m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 7,
                column: "Price",
                value: 1800m);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 8,
                column: "Price",
                value: 1900m);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 9,
                column: "Price",
                value: 1600m);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 10,
                column: "Price",
                value: 1700m);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 11,
                columns: new[] { "Description", "Price" },
                values: new object[] { "Effizienter Stadtturbomotor.", 1800m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 12,
                columns: new[] { "Description", "Price" },
                values: new object[] { "Sportlicher Turbomotor.", 2400m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 13,
                columns: new[] { "Description", "Price" },
                values: new object[] { "Gleichmäßige LED-Ausleuchtung.", 980m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 14,
                columns: new[] { "Description", "Price" },
                values: new object[] { "Erweiterte Reichweite bei Nacht.", 1150m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 15,
                columns: new[] { "Description", "Price" },
                values: new object[] { "Effizienter Hybridmotor mit ruhigem Lauf.", 3900m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 16,
                columns: new[] { "Description", "Price" },
                values: new object[] { "Extrem helle LED-Scheinwerfer für maximale Sicht.", 1750m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 17,
                column: "Price",
                value: 2100m);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 18,
                column: "Price",
                value: 1650m);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 19,
                columns: new[] { "Description", "Price" },
                values: new object[] { "Hybridantrieb mit niedrigem Verbrauch.", 2600m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 20,
                columns: new[] { "Description", "Price" },
                values: new object[] { "Blendfreies Fernlicht mit Pixel-Technologie.", 1350m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 21,
                columns: new[] { "Description", "Price" },
                values: new object[] { "Leichte Schmiedefelge mit hoher Stabilität.", 1350m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 22,
                columns: new[] { "Description", "Price" },
                values: new object[] { "Klassische Alufelge für Alltag und Komfort.", 850m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 23,
                columns: new[] { "Description", "Price" },
                values: new object[] { "Sportliche Mehrspeichenfelge.", 980m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 24,
                columns: new[] { "Description", "Price" },
                values: new object[] { "Schwarze Performance-Felge.", 1100m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 30,
                column: "Description",
                value: "Sportlicher Carbon-Heckspoiler für verbesserte Aerodynamik.");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 31,
                column: "Description",
                value: "Kompakter Sportspoiler für den Polo.");
        }
    }
}
