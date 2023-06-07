CREATE DATABASE  IF NOT EXISTS `pharmdb` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `pharmdb`;
-- MySQL dump 10.13  Distrib 5.6.13, for Win32 (x86)
--
-- Host: localhost    Database: pharmdb
-- ------------------------------------------------------
-- Server version	5.6.17

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `catedep`
--

DROP TABLE IF EXISTS `catedep`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `catedep` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `cat` varchar(45) DEFAULT NULL,
  `libelle` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=210 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `catedep`
--

LOCK TABLES `catedep` WRITE;
/*!40000 ALTER TABLE `catedep` DISABLE KEYS */;
INSERT INTO `catedep` VALUES (2,'ADMINISTRATION','Communications'),(3,'ADMINISTRATION','Frais banque (agio)'),(4,'ADMINISTRATION','Cotisations-Taxes'),(7,'PERSONNEL','Prêts au personnel'),(8,'PERSONNEL','Primes/Indemnités'),(12,'IMMOBILISATION','Constructions/Rénovation'),(13,'IMMOBILISATION','Réévaluation de la propriété'),(14,'IMMOBILISATION','Equipements/ Mobilier'),(15,'IMMOBILISATION','Véhicule'),(16,'PERSONNEL','Autres prêts'),(17,'PERSONNEL','Salaires ou rémunération(laïcs et sœurs)'),(19,'PERSONNEL','Remboursement'),(23,'CONSOMMABLES','Electricité'),(24,'CONSOMMABLES','Electricité'),(25,'CONSOMMABLES','carburant  generateur, gaz'),(26,'CONSOMMABLES','eau'),(27,'CONSOMMABLES','Entretien-Petit Materiel/reparation'),(28,'CONSOMMABLES','Voyage-Transport'),(29,'CONSOMMABLES','Medicaments-consommables'),(30,'CONSOMMABLES','Reactifs-Consommable Labo'),(31,'CONSOMMABLES','Carnet-Fiches-Mat Active'),(32,'CONSOMMABLES','Echographie/ Radiologie'),(33,'CONSOMMABLES','Activite/Farine/Sirop'),(34,'CONSOMMABLES','Culte'),(35,'CONSOMMABLES','Relation Sociales'),(36,'CONSOMMABLES','Alimentation /malades /hospi'),(37,'CONSOMMABLES','Soutien Aux Malades'),(38,'CONSOMMABLES','Prets/Remboursements'),(39,'CONSOMMABLES','Pertes Vols/peremption'),(40,'CONSOMMABLES','Autre  CPE'),(41,'ADMINISTRATION','Credit SNE'),(42,'CONSOMMABLES','papier ram'),(43,'CONSOMMABLES','litre de super'),(46,'CONSOMMABLES','sucre'),(47,'CONSOMMABLES','farine'),(48,'CONSOMMABLES','tambour et encre'),(49,'CONSOMMABLES','telephone garede infirmier'),(50,'CONSOMMABLES','credit garde infirmier'),(51,'CONSOMMABLES','facture film radio'),(52,'CONSOMMABLES','pains'),(53,'CONSOMMABLES','gaz vaccin'),(54,'ADMINISTRATION','droit opj'),(55,'ADMINISTRATION','credit communication'),(56,'ADMINISTRATION','credit flotte pharmacie'),(57,'ADMINISTRATION','credit flotte cnt'),(58,'IMMOBILISATION','plomberie'),(59,'CONSOMMABLES','gaz oil'),(60,'CONSOMMABLES','balai'),(61,'CONSOMMABLES','achat groupe electrogene120 kw'),(62,'CONSOMMABLES','carburant ambulance'),(63,'CONSOMMABLES','aspirateur chirurgical'),(64,'ADMINISTRATION','credit flotte labo'),(65,'ADMINISTRATION','achat  ampoules'),(66,'CONSOMMABLES','achat medicament augustin'),(67,'ADMINISTRATION','reliure'),(68,'ADMINISTRATION','reparation moulin'),(69,'CONSOMMABLES','Carburant Pick-up'),(70,'IMMOBILISATION','Entretien  et maintenance'),(71,'CONSOMMABLES','Achat Thermoetres UNT'),(72,'CONSOMMABLES','Recharges gaz'),(73,'CONSOMMABLES','Achat multiprises'),(74,'CONSOMMABLES','Cadenas et balaie'),(75,'CONSOMMABLES','enveloppe'),(76,'CONSOMMABLES','tete de loup'),(77,'CONSOMMABLES','jus'),(78,'CONSOMMABLES','manches des ballets'),(79,'CONSOMMABLES','seaux'),(80,'CONSOMMABLES','blouses'),(81,'ADMINISTRATION','salaire en espece'),(82,'ADMINISTRATION','salaire par virement'),(83,'AUTRES','vidange fosse UNT'),(84,'ADMINISTRATION','credit soeur emi'),(85,'ADMINISTRATION','credit soeur felicia'),(86,'CONSOMMABLES','registres des courriers'),(87,'CONSOMMABLES','pret a soeur odile'),(88,'COMMUNAUTE','tabouret roulant'),(89,'ADMINISTRATION','main d\'oeuvre peintre'),(90,'CONSOMMABLES','roulettes'),(91,'ADMINISTRATION','assurance pickup'),(94,'COMMUNAUTE','transfert miandbe'),(95,'IMMOBILISATION','transport hortense'),(96,'CONSOMMABLES','onduleur'),(100,'COMMUNAUTE','nescafe et thé'),(101,'COMMUNAUTE','raclette'),(102,'COMMUNAUTE','main d\'oeuvre ouvriers'),(103,'COMMUNAUTE','petites piles'),(105,'CONSOMMABLES','Sucrerie pour la reunion'),(106,'CONSOMMABLES','Consommable informatique'),(107,'PERSONNEL','Prèt à Ouang labo '),(108,'CONSOMMABLES','Achat clim'),(123,'ADMINISTRATION','REMBOURSEMENT IRC'),(124,'ADMINISTRATION','REMBOURSEMENT SOIN MR PAUL'),(125,'IMMOBILISATION','ENTRETIEN BATIMENT MATERNITE'),(127,'CONSOMMABLES','CREDIT INTERNET'),(128,'CONSOMMABLES','DON DE PAQUE'),(131,'CONSOMMABLES','ENCRE IMP UNT'),(132,'ADMINISTRATION','DON DE PAQUE'),(133,'CONSOMMABLES','CREDIT INTERNET UNT'),(137,'ADMINISTRATION','FACTURE MAHAMAT'),(138,'ADMINISTRATION','FACTURE TRACTAFRIC'),(139,'ADMINISTRATION','FORTAIT JOURNALISTE'),(140,'CONSOMMABLES','GAZ UNT'),(141,'ADMINISTRATION','SALAIRE GENDARMES'),(142,'IMMOBILISATION','Menuiserie (Armoire Bloc)'),(143,'ADMINISTRATION','Carburant Rav4'),(144,'ADMINISTRATION','Carburant navara'),(145,'CONSOMMABLES','Encre imp et copieur'),(146,'','Matériel moulin'),(147,'','Achat médicament'),(148,'IMMOBILISATION','Extracteur d\'oxygène'),(149,'CONSOMMABLES','Papier écho/radio'),(150,'CONSOMMABLES','Papier écho/radio'),(151,'CONSOMMABLES','Papier hygiénique'),(152,'CONSOMMABLES','Papier écho/radio'),(153,'CONSOMMABLES','Papier hygiénique'),(154,'IMMOBILISATION','Facture laborex'),(155,'AUTRES','Billet voyage soeur'),(156,'AUTRES','Anternet (routeur)'),(157,'IMMOBILISATION','Machine reliure'),(158,'IMMOBILISATION','Routeur wifi'),(159,'CONSOMMABLES','Film echo/radio'),(160,'IMMOBILISATION','Stérilisateur'),(161,'CONSOMMABLES','produits d\'entretien'),(162,'ADMINISTRATION','Facture internet'),(163,'ADMINISTRATION','Entretien informatique'),(164,'ADMINISTRATION','Douane colis'),(170,'MAISON','Tissu Sr Cecile'),(171,'MAISON','Batterie Bousso'),(173,'MAISON','Tissu Sr Cecile'),(176,'MAISON','Entretien chateau Bousso'),(177,'MAISON','Tissu Sr Cecile'),(178,'MAISON','Retraite sr Léa'),(181,'CONSOMMABLES','pochette de plastification'),(182,'CONSOMMABLES','Papier cartonné'),(184,'CONSOMMABLES','Agrafeuse bte de 10'),(185,'IMMOBILISATION','Moniteur BIOCAR'),(186,'IMMOBILISATION','Matériel Bloc'),(187,'IMMOBILISATION','reparation pickup'),(188,'COMMUNAUTE','billet congo espèce'),(190,'COMMUNAUTE','Biellet congo chèque'),(191,'AUTRES','pèse personne'),(192,'IMMOBILISATION','Entretien voiture'),(193,'IMMOBILISATION','Achat copieur'),(194,'IMMOBILISATION','Appareil pulverisation'),(195,'IMMOBILISATION','Batiment Raoul Foll'),(196,'IMMOBILISATION','Achat pneu amb'),(197,'CONSOMMABLES','remis à Isidor'),(198,'CONSOMMABLES','achat et fabrication perles'),(199,'IMMOBILISATION','Droit grue'),(201,'IMMOBILISATION','Inspection douane'),(202,'CONSOMMABLES','Menuiserie'),(203,'IMMOBILISATION','Achat terrain mogo'),(204,'','achat dijoncteur FRF'),(205,'IMMOBILISATION','Achat machine à laver'),(206,'CONSOMMABLES','Vidange amb'),(207,'ADMINISTRATION','Don à St Luc'),(208,'PERSONNEL','grrr'),(209,'CONSOMMABLES','Alimentation /malades /hospijjgkkkkkkkkkkkkkk');
/*!40000 ALTER TABLE `catedep` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2019-01-24 12:59:32
