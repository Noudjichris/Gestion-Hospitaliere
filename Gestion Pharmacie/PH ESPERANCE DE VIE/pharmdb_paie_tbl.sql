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
-- Table structure for table `paie_tbl`
--

DROP TABLE IF EXISTS `paie_tbl`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `paie_tbl` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `date_paye` datetime DEFAULT NULL,
  `montant` decimal(11,0) DEFAULT NULL,
  `num_empl` varchar(20) DEFAULT NULL,
  `id_client` int(11) DEFAULT NULL,
  `etat` bit(1) DEFAULT NULL,
  `num_vente` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `KeyEmployeKey_idx` (`num_empl`),
  KEY `KeyClientKey_idx` (`id_client`),
  KEY `key_idx` (`num_vente`),
  CONSTRAINT `KeyClientKey` FOREIGN KEY (`id_client`) REFERENCES `clienttbl` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `KeyEmployeKey` FOREIGN KEY (`num_empl`) REFERENCES `employe` (`num_empl`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `keyVebtePaie` FOREIGN KEY (`num_vente`) REFERENCES `detail_vente` (`num_vente`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `paie_tbl`
--

LOCK TABLES `paie_tbl` WRITE;
/*!40000 ALTER TABLE `paie_tbl` DISABLE KEYS */;
INSERT INTO `paie_tbl` VALUES (2,'2019-05-25 00:00:00',1000,'103',1272,'',2),(3,'2019-05-25 00:00:00',900,'103',1272,'',3);
/*!40000 ALTER TABLE `paie_tbl` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2019-05-25 18:23:36
