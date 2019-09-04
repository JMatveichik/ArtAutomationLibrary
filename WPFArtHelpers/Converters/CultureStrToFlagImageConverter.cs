using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace ArtWPFHelpers.Converters
{
    [ValueConversion(typeof(string), typeof(string))]
    public class CultureStrToFlagImageConverter : IValueConverter
    {
        static CultureStrToFlagImageConverter()
        {
            CultureImages.Add("rus", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_russia.ico");
            CultureImages.Add("eng", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_usa.ico");
            /*
            CultureImages.Add("AFG", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_afghanistan.ico");//Afghanistan
            CultureImages.Add("ALB", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_albania.ico");//Albania
            CultureImages.Add("DZA", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_algeria.ico");//Algeria
            CultureImages.Add("ASM", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_american_samoa.ico");//American Samoa
            CultureImages.Add("AND", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_andorra.ico");//Andorra
            CultureImages.Add("AGO", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_angola.ico");//Angola
            CultureImages.Add("AIA", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_anguilla.ico");//Anguilla
            CultureImages.Add("ATG", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_antigua_and_barbuda.ico");//Antigua and Barbuda
            CultureImages.Add("ARG", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_argentina.ico");//Argentina
            CultureImages.Add("ARM", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_armenia.ico");//Armenia
            CultureImages.Add("ABW", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_aruba.ico");//Aruba
            CultureImages.Add("AUS", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_australia.ico");//Australia
            CultureImages.Add("AUT", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_austria.ico");//Austria
            CultureImages.Add("AZE", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_azerbaijan.ico");//Azerbaijan
            CultureImages.Add("BHS", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_bahamas.ico");//Bahamas
            CultureImages.Add("BHR", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_bahrain.ico");//Bahrain
            CultureImages.Add("BGD", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_bangladesh.ico");//Bangladesh
            CultureImages.Add("BRB", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_barbados.ico");//Barbados
            CultureImages.Add("BLR", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_belarus.ico");//Belarus
            CultureImages.Add("BEL", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_belgium.ico");//Belgium
            CultureImages.Add("BLZ", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_belize.ico");//Belize
            CultureImages.Add("BEN", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_benin.ico");//Benin
            CultureImages.Add("BMU", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_bermuda.ico");//Bermuda
            CultureImages.Add("BTN", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_bhutan.ico");//Bhutan
            CultureImages.Add("BOL", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_bolivia.ico");//Bolivia
            CultureImages.Add("BIH", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_bosnia_and_herzegovina.ico");//Bosnia and Herzegovina
            CultureImages.Add("BWA", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_botswana.ico");//Botswana
            CultureImages.Add("BRA", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_brazil.ico");//Brazil
            CultureImages.Add("IOT", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_british_indian_ocean_territory.ico");//British Indian Ocean Territory
            CultureImages.Add("BRN", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_brunei.ico");//Brunei Darussalam
            CultureImages.Add("BGR", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_bulgaria.ico");//Bulgaria
            CultureImages.Add("BFA", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_burkina_faso.ico");//Burkina Faso
            CultureImages.Add("BDI", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_burundi.ico");//Burundi
            CultureImages.Add("KHM", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_cambodia.ico");//Cambodia
            CultureImages.Add("CMR", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_cameroon.ico");//Cameroon
            CultureImages.Add("CAN", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_canada.ico");//Canada
            CultureImages.Add("CPV", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_cape_verde.ico");//Cape Verde
            CultureImages.Add("CYM", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_cayman_islands.ico");//Cayman Islands
            CultureImages.Add("CAF", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_central_african_republic.ico");//Central African Republic
            CultureImages.Add("TCD", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_chad.ico");//Chad
            CultureImages.Add("CHL", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_chile.ico");//Chile
            CultureImages.Add("CHN", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_china.ico");//China
            CultureImages.Add("COL", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_colombia.ico");//Colombia
            CultureImages.Add("COM", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_comoros.ico");//Comoros
            CultureImages.Add("COG", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_congo_democratic_republic.ico");//Congo
            CultureImages.Add("COD", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_congo_republic.ico");//Democratic Republic of the Congo
            CultureImages.Add("COK", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_cook_islands.ico");//Cook Islands
            CultureImages.Add("CRI", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_costa_rica.ico");//Costa Rica
            CultureImages.Add("HRV", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_cote_divoire.ico");//Croatia
            CultureImages.Add("CUB", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_croatia.ico");//Cuba
            CultureImages.Add("CUW", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_cuba.ico");//Curacao
            CultureImages.Add("CYP", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_cyprus.ico");//Cyprus
            CultureImages.Add("CZE", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_czech_republic.ico");//Czech Republic
            CultureImages.Add("DNK", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_denmark.ico");//Denmark
            CultureImages.Add("DJI", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_djibouti.ico");//Djibouti
            CultureImages.Add("DMA", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_dominica.ico");//Dominica
            CultureImages.Add("DOM", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_dominican_republic.ico");//Dominican Republic
            CultureImages.Add("ECU", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_ecuador.ico");//Ecuador
            CultureImages.Add("EGY", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_egypt.ico");//Egypt
            CultureImages.Add("SLV", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_el_salvador.ico");//El Salvador
            CultureImages.Add("GNQ", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_england.ico");//Equatorial Guinea
            CultureImages.Add("ERI", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_equatorial_guinea.ico");//Eritrea
            CultureImages.Add("EST", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_eritrea.ico");//Estonia
            CultureImages.Add("ETH", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_estonia.ico");//Ethiopia
            CultureImages.Add("FLK", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_ethiopia.ico");//Falkland Islands (Malvinas)
            CultureImages.Add("FRO", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_eu.ico");//Faroe Islands
            CultureImages.Add("FJI", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_faeroe_islands.ico");//Fiji
            CultureImages.Add("FIN", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_fiji.ico");//Finland
            CultureImages.Add("FRA", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_finland.ico");//France
            CultureImages.Add("GUF", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_france.ico");//French Guiana
            CultureImages.Add("PYF", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_french_polynesia.ico");//French Polynesia
            CultureImages.Add("GAB", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_gabon.ico");//Gabon
            CultureImages.Add("GMB", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_gambia.ico");//Gambia
            CultureImages.Add("GEO", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_georgia.ico");//Georgia
            CultureImages.Add("DEU", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_germany.ico");//Germany
            CultureImages.Add("GHA", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_ghana.ico");//Ghana
            CultureImages.Add("GIB", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_gibraltar.ico");//Gibraltar
            CultureImages.Add("GRC", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_greece.ico");//Greece
            CultureImages.Add("GRL", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_greenland.ico");//Greenland
            CultureImages.Add("GRD", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_grenada.ico");//Grenada
            CultureImages.Add("GUM", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_guam.ico");//Guam
            CultureImages.Add("GTM", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_guatemala.ico");//Guatemala
            CultureImages.Add("GGY", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_guernsey.ico");//Guernsey
            CultureImages.Add("GIN", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_guinea.ico");//Guinea
            CultureImages.Add("GNB", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_guinea_bissau.ico");//Guinea-Bissau
            CultureImages.Add("GUY", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_guyana.ico");//Guyana
            CultureImages.Add("HTI", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_haiti.ico");//Haiti
            CultureImages.Add("HND", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_honduras.ico");//Honduras
            CultureImages.Add("HKG", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_hong_kong.ico");//Hong Kong
            CultureImages.Add("HUN", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_hungary.ico");//Hungary
            CultureImages.Add("ISL", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_iceland.ico");//Iceland
            CultureImages.Add("IND", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_india.ico");//India
            CultureImages.Add("IDN", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_indonesia.ico");//Indonesia
            CultureImages.Add("IRN", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_iran.ico");//Iran, Islamic Republic of
            CultureImages.Add("IRQ", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_iraq.ico");//Iraq
            CultureImages.Add("IRL", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_ireland.ico");//Ireland
            CultureImages.Add("IMN", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_isle_of_man.ico");//Isle of Man
            CultureImages.Add("ISR", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_israel.ico");//Israel
            CultureImages.Add("ITA", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_italy.ico");//Italy
            CultureImages.Add("JAM", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_jamaica.ico");//Jamaica
            CultureImages.Add("JPN", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_japan.ico");//Japan
            CultureImages.Add("JEY", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_jersey.ico");//Jersey
            CultureImages.Add("JOR", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_jordan.ico");//Jordan
            CultureImages.Add("KAZ", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_kazakhstan.ico");//Kazakhstan
            CultureImages.Add("KEN", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_kenya.ico");//Kenya
            CultureImages.Add("KIR", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_kiribati.ico");//Kiribati
            CultureImages.Add("KWT", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_kuwait.ico");//Kuwait
            CultureImages.Add("KGZ", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_kyrgyzstan.ico");//Kyrgyzstan
            CultureImages.Add("LAO", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_laos.ico");//Lao People's Democratic Republic
            CultureImages.Add("LVA", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_latvia.ico");//Latvia
            CultureImages.Add("LBN", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_lebanon.ico");//Lebanon
            CultureImages.Add("LSO", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_lesotho.ico");//Lesotho
            CultureImages.Add("LBR", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_liberia.ico");//Liberia
            CultureImages.Add("LBY", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_libya.ico");//Libya
            CultureImages.Add("LIE", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_liechtenstein.ico");//Liechtenstein
            CultureImages.Add("LTU", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_lithuania.ico");//Lithuania
            CultureImages.Add("LUX", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_luxembourg.ico");//Luxembourg
            CultureImages.Add("MAC", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_macau.ico");//Macao
            CultureImages.Add("MKD", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_macedonia.ico");//Macedonia, the Former Yugoslav Republic of
            CultureImages.Add("MDG", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_madagascar.ico");//Madagascar
            CultureImages.Add("MWI", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_malawi.ico");//Malawi
            CultureImages.Add("MYS", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_malaysia.ico");//Malaysia
            CultureImages.Add("MDV", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_maldives.ico");//Maldives
            CultureImages.Add("MLI", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_mali.ico");//Mali
            CultureImages.Add("MLT", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_malta.ico");//Malta
            CultureImages.Add("MHL", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_marshall_islands.ico");//Marshall Islands
            CultureImages.Add("MTQ", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_martinique.ico");//Martinique
            CultureImages.Add("MRT", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_mauritania.ico");//Mauritania
            CultureImages.Add("MUS", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_mauritius.ico");//Mauritius
            CultureImages.Add("MEX", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_mexico.ico");//Mexico
            CultureImages.Add("FSM", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_micronesia.ico");//Micronesia, Federated States of
            CultureImages.Add("MDA", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_moldova.ico");//Moldova, Republic of
            CultureImages.Add("MCO", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_monaco.ico");//Monaco
            CultureImages.Add("MNG", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_mongolia.ico");//Mongolia
            CultureImages.Add("MNE", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_montenegro.ico");//Montenegro
            CultureImages.Add("MSR", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_montserrat.ico");//Montserrat
            CultureImages.Add("MAR", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_morocco.ico");//Morocco
            CultureImages.Add("MOZ", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_mozambique.ico");//Mozambique
            CultureImages.Add("NAM", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_namibia.ico");//Namibia
            CultureImages.Add("NRU", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_nauru.ico");//Nauru
            CultureImages.Add("NPL", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_nepal.ico");//Nepal
            CultureImages.Add("NLD", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_netherlands.ico");//Netherlands
            CultureImages.Add("NCL", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_netherlands_antilles.ico");//New Caledonia
            CultureImages.Add("NZL", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_new_zealand.ico");//New Zealand
            CultureImages.Add("NIC", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_nicaragua.ico");//Nicaragua
            CultureImages.Add("NER", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_niger.ico");//Niger
            CultureImages.Add("NGA", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_nigeria.ico");//Nigeria
            CultureImages.Add("NIU", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_niue.ico");//Niue
            CultureImages.Add("NFK", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_norfolk_island.ico");//Norfolk Island
            CultureImages.Add("MNP", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_northern_mariana_islands.ico");//Northern Mariana Islands
            CultureImages.Add("NOR", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_norway.ico");//Norway
            CultureImages.Add("OMN", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_oman.ico");//Oman
            CultureImages.Add("PAK", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_pakistan.ico");//Pakistan
            CultureImages.Add("PLW", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_palau.ico");//Palau
            CultureImages.Add("PAN", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_panama.ico");//Panama
            CultureImages.Add("PNG", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_papua_new_guinea.ico");//Papua New Guinea
            CultureImages.Add("PRY", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_paraguay.ico");//Paraguay
            CultureImages.Add("PER", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_peru.ico");//Peru
            CultureImages.Add("PHL", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_philippines.ico");//Philippines
            CultureImages.Add("PCN", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_pitcairn_islands.ico");//Pitcairn
            CultureImages.Add("POL", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_poland.ico");//Poland
            CultureImages.Add("PRT", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_portugal.ico");//Portugal
            CultureImages.Add("PRI", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_puerto_rico.ico");//Puerto Rico
            CultureImages.Add("QAT", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_qatar.ico");//Qatar
            CultureImages.Add("ROU", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_romania.ico");//Romania
            CultureImages.Add("RUS", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_russia.ico");//Russian Federation
            CultureImages.Add("RWA", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_rwanda.ico");//Rwanda
            CultureImages.Add("SHN", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_saint_helena.ico");//Saint Helena
            CultureImages.Add("KNA", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_saint_kitts_and_nevis.ico");//Saint Kitts and Nevis
            CultureImages.Add("LCA", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_saint_lucia.ico");//Saint Lucia
            CultureImages.Add("SPM", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_saint_pierre_and_miquelon.ico");//Saint Pierre and Miquelon
            CultureImages.Add("VCT", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_saint_vincent_and_the_grenadines.ico");//Saint Vincent and the Grenadines
            CultureImages.Add("WSM", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_samoa.ico");//Samoa
            CultureImages.Add("SMR", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_san_marino.ico");//San Marino
            CultureImages.Add("STP", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_sao_tome_and_principe.ico");//Sao Tome and Principe
            CultureImages.Add("SAU", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_saudi_arabia.ico");//Saudi Arabia
            CultureImages.Add("SEN", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_senegal.ico");//Senegal
            CultureImages.Add("SRB", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_serbia.ico");//Serbia
            CultureImages.Add("SYC", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_seychelles.ico");//Seychelles
            CultureImages.Add("SLE", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_sierra_leone.ico");//Sierra Leone
            CultureImages.Add("SGP", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_singapore.ico");//Singapore
            CultureImages.Add("SVK", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_slovakia.ico");//Slovakia
            CultureImages.Add("SVN", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_slovenia.ico");//Slovenia
            CultureImages.Add("SLB", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_solomon_islands.ico");//Solomon Islands
            CultureImages.Add("SOM", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_somalia.ico");//Somalia
            CultureImages.Add("ZAF", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_south_africa.ico");//South Africa
            CultureImages.Add("SGS", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_south_georgia.ico");//South Georgia and the South Sandwich Islands
            CultureImages.Add("ESP", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_spain.ico");//Spain
            CultureImages.Add("LKA", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_sri_lanka.ico");//Sri Lanka
            CultureImages.Add("SDN", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_sudan.ico");//Sudan
            CultureImages.Add("SUR", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_suriname.ico");//Suriname
            CultureImages.Add("SWZ", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_swaziland.ico");//Swaziland
            CultureImages.Add("SWE", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_sweden.ico");//Sweden
            CultureImages.Add("CHE", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_switzerland.ico");//Switzerland
            CultureImages.Add("SYR", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_syria.ico");//Syrian Arab Republic
            CultureImages.Add("TWN", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_taiwan.ico");//Taiwan
            CultureImages.Add("TJK", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_tajikistan.ico");//Tajikistan
            CultureImages.Add("TZA", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_tanzania.ico");//United Republic of Tanzania
            CultureImages.Add("THA", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_thailand.ico");//Thailand
            CultureImages.Add("TLS", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_tibet.ico");//Timor-Leste
            CultureImages.Add("TGO", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_togo.ico");//Togo
            CultureImages.Add("TKL", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_tonga.ico");//Tokelau
            CultureImages.Add("TON", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_trinidad_and_tobago.ico");//Tonga
            CultureImages.Add("TTO", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_tunisia.ico");//Trinidad and Tobago
            CultureImages.Add("TUN", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_turkey.ico");//Tunisia
            CultureImages.Add("TUR", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_turkmenistan.ico");//Turkey
            CultureImages.Add("TKM", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_turks_and_caicos_islands.ico");//Turkmenistan
            CultureImages.Add("TUV", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_tuvalu.ico");//Tuvalu
            CultureImages.Add("UGA", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_uganda.ico");//Uganda
            CultureImages.Add("UKR", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_ukraine.ico");//Ukraine
            CultureImages.Add("ARE", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_united_arab_emirates.ico");//United Arab Emirates
            CultureImages.Add("GBR", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_united_kingdom.ico");//United Kingdom
            CultureImages.Add("USA", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_usa.ico");//United States
            CultureImages.Add("UZB", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_uzbekistan.ico");//Uzbekistan
            CultureImages.Add("VUT", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_vanuatu.ico");//Vanuatu
            CultureImages.Add("VEN", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_venezuela.ico");//Venezuela
            CultureImages.Add("VNM", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_vietnam.ico");//Viet Nam
            CultureImages.Add("VGB", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_virgin_islands.ico");//British Virgin Islands
            CultureImages.Add("WLF", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_wallis_and_futuna.ico");//Wallis and Futuna
            CultureImages.Add("YEM", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_yemen.ico");//Yemen
            CultureImages.Add("ZMB", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_zambia.ico");//Zambia
            CultureImages.Add("ZWE", "pack://application:,,,/ArtWPFHelpers;component/Resources/Flags/flag_zimbabwe.ico");//Zimbabwe
            */


        }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string cult = value as string;
            cult = cult.ToUpper();
            if (CultureImages.ContainsKey(cult))
                return CultureImages[cult];

            return null;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }

        static private Dictionary<string, string> CultureImages = new Dictionary<string, string>();

    }
}
