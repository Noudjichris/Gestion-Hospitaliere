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
-- Table structure for table `liste_exam_tbl`
--

DROP TABLE IF EXISTS `liste_exam_tbl`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `liste_exam_tbl` (
  `num_exam` int(11) NOT NULL AUTO_INCREMENT,
  `type_exam` varchar(45) NOT NULL,
  `descript` varchar(150) DEFAULT NULL,
  `prix` decimal(10,0) DEFAULT NULL,
  PRIMARY KEY (`num_exam`,`type_exam`)
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `liste_exam_tbl`
--

LOCK TABLES `liste_exam_tbl` WRITE;
/*!40000 ALTER TABLE `liste_exam_tbl` DISABLE KEYS */;
INSERT INTO `liste_exam_tbl` VALUES (1,'ACIDE URIQUE UA AU','EXAMEN POUR.....',5000),(2,'ALT','',5000),(3,'ASLO','',5000),(4,'AST','',5000),(5,'BBTS','',5000),(6,'CALCIUM','',5000),(7,'CREA','',5000),(9,'CRP','',5000),(10,'ECBU','',3000),(11,'GE','',3000),(12,'GLYCEMIE JAUNE','',5000),(13,'ECOGRAPHIE','',7000),(14,'RADIO X','',5000);
/*!40000 ALTER TABLE `liste_exam_tbl` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2019-01-24 12:59:36
